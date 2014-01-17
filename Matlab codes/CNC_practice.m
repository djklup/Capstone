function [perc] = CNC_practice(baseatten)
%Use this function to present CNC words via MATLAB - practice
%e.g. CNC_practice(30); %for 30 dB base attenuation.
%If no parameters are entered, defaults for remaining parameter:
%  baseatten=21 (60 dBa).

if nargin<1,
    baseatten=21;   %update this to correct atten for desired SPL
end

soundPath='/SoundFiles/CNC/MSTB_CD/';
feval('cd',soundPath);

%Connect to PA5
PA5=actxcontrol('PA5.x',[5 5 26 26]);
invoke(PA5,'ConnectPA5','USB',1);
PA5_2=actxcontrol('PA5.x',[10 5 36 26]);
invoke(PA5_2,'ConnectPA5','USB',2);

%Set attens
PA5.SetAtten(baseatten); 
errorl=PA5.GetError();
if length(errorl)~=0
    PA5.Display(errorl, 0);
end
PA5_2.SetAtten(baseatten);   
errorl=PA5_2.GetError();
if length(errorl)~=0
    PA5_2.Display(errorl, 0);
end


cr=status_bar;  % initializing the current run status bar
%loading and executing the "new" GUI derived from matlab:
load ark1.mat; 
h=ark1cnc;


disp('Now playing practice words...');
Nwordtot=0;
Cwordtot=0;
AllN=0;
Alltot=0;

%Load list audio and text files
SoundFile=sprintf('%sTrack27.wav',soundPath);
coli=2;
sent={'1.  d u ck', '2.  b o mb', '3.  j u ne'};
parseN=[2.5e5 4.2e5 5.9e5];

for j=1:3
    set(cr,'String',num2str(j));
    %Read and print out sentence words from list
    Nwords=0;
    Allcorrect=0;
    [T,R]=strtok(sent{j},'.');
    [T,R]=strtok(R);
    for k=1:5
        [T,R]=strtok(R);
        words{k}=T;
        if ~isempty(T)
            Nwords=Nwords+1;
        end
        h1 = uicontrol('Parent',h, ...
            'Units','points', ...
            'BackgroundColor',red, ...
            'Callback',sprintf(' shp=%i; ',k),  ...
            'FontSize',20, ...
            'Position',buttoncnc(k).position, ...
            'String',words{k}, ...
            'Tag','Pushbutton1');
    end
    h1 = uicontrol('Parent',h, ...
        'Units','points', ...
        'BackgroundColor',red, ...
        'Callback',' shp=7; ',  ...
        'FontSize',24, ...
        'Position',buttoncnc(7).position, ...
        'String','Done', ...
        'Tag','Pushbutton1');
    
    pause(0.5);
    %Play sentence
    N1=parseN(j)-90000;
    N2=parseN(j)+60000;
    [Y,Fs]=wavread(SoundFile,[N1,N2]);
    Y=[Y(:,coli) zeros(N2-N1+1,1)];
    %figure(100); plot(Y);
    wavplay(Y,Fs);
    %Get words correct from experimenter and save to file
    Wcorrect=zeros(1,5);
    shpdone=0;
    while (shpdone==0)
        waitbutton;
        answer=shp;
        if answer==7
            shpdone=1;
        elseif answer<=Nwords
            if Wcorrect(answer)==0  %change button color to blue for correct
                h1 = uicontrol('Parent',h, ...
                    'Units','points', ...
                    'BackgroundColor',blue, ...
                    'Callback',sprintf(' shp=%i; ',answer),  ...
                    'FontSize',20, ...
                    'Position',buttoncnc(answer).position, ...
                    'String',words{answer}, ...
                    'Tag','Pushbutton1');
                Wcorrect(answer)=1;
            else Wcorrect(answer)==1  %change button color back to red for incorrect
                h1 = uicontrol('Parent',h, ...
                    'Units','points', ...
                    'BackgroundColor',red, ...
                    'Callback',sprintf(' shp=%i; ',answer),  ...
                    'FontSize',20, ...
                    'Position',buttoncnc(answer).position, ...
                    'String',words{answer}, ...
                    'Tag','Pushbutton1');
                Wcorrect(answer)=0;
            end
        elseif answer==6
            for ai=1:Nwords
                if Wcorrect(ai)==0
                    h1 = uicontrol('Parent',h, ...
                        'Units','points', ...
                        'BackgroundColor',blue, ...
                        'Callback',sprintf(' shp=%i; ',ai),  ...
                        'FontSize',20, ...
                        'Position',buttoncnc(ai).position, ...
                        'String',words{ai}, ...
                        'Tag','Pushbutton1');
                    Wcorrect(ai)=1;
                else Wcorrect(ai)==1  %change button color back to red for incorrect
                    h1 = uicontrol('Parent',h, ...
                        'Units','points', ...
                        'BackgroundColor',red, ...
                        'Callback',sprintf(' shp=%i; ',ai),  ...
                        'FontSize',20, ...
                        'Position',buttoncnc(ai).position, ...
                        'String',words{ai}, ...
                        'Tag','Pushbutton1');
                    Wcorrect(ai)=0;
                end
            end
        elseif answer==8
            wavplay(Y,Fs);
        end
        Ncorrect=sum(Wcorrect);
        if Ncorrect==Nwords
            Allcorrect=1;
        end
        Ncorrectstr=sprintf('N=%i',Ncorrect);
        h1 = uicontrol('Parent',h, ...
            'Units','points', ...
            'BackgroundColor',red, ...
            'Callback',' shp=7; ',  ...
            'FontSize',24, ...
            'Position',buttoncnc(7).position, ...
            'String',Ncorrectstr, ...
            'Tag','Pushbutton1');
    end
    wcorrectstr='';
    for m=1:5
        if Wcorrect(m)==1
            wcorrectstr=sprintf('%s%s,',wcorrectstr,words{m});
        end
    end
    Nwordtot=Nwordtot+Nwords;
    Cwordtot=Cwordtot+Ncorrect;
    AllN=AllN+1;
    Alltot=Alltot+Allcorrect;
    %         disp(sprintf('Word %i of List %i: Hit enter to continue',j,i));
    %         pause;
end
perc=Cwordtot/Nwordtot*100;
percA=Alltot/AllN*100;
disp(sprintf('Total correct: %i of %i = %.1f percent Phonemes correct.\n',Cwordtot,Nwordtot,perc));
disp(sprintf('Total correct: %i of %i = %.1f percent Words correct.\n',Alltot,AllN,percA));



close all;
