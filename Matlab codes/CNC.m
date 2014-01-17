function [perc] = CNC(subjID,lists,baseatten)
%Use this function to present CNC words via MATLAB
%e.g. CNC('test',[1 10],30); %for subject 'test', lists 1 and 10,
%                                 %  30 dB base attenuation.
%If only two parameters are entered, defaults for remaining parameter:
%  baseatten=21 (60 dBa).

if nargin<3,
    baseatten=21;   %update this to correct atten for desired SPL
end

soundPath='/SoundFiles/CNC/MSTB_CD/';
pathSave=['/Experiments/Data/' subjID '/'];
feval('cd',soundPath);
load CNCparseset2;

if exist(pathSave)~=7   %if file directory doesn't exist, create new dir
    success=mkdir(['/Experiments/Data/'],subjID);
    if success==0
        disp('Create directory failed!  Aborting...'); 
        return; 
    end
end
feval('cd',pathSave);

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
PA5_2.SetAtten(99.0);   
errorl=PA5_2.GetError();
if length(errorl)~=0
    PA5_2.Display(errorl, 0);
end


flist=dir; 
flist=flist(3:end); 
subjID=GetFolder(subjID);
outFile=fileExistCheck(flist,...
     [subjID '_CNC_0.txt']);
fid=fopen(outFile,'wt');
disp(sprintf('saved to %s \n',[pathSave outFile]));


cr=status_bar;  % initializing the current run status bar
%loading and executing the "new" GUI derived from matlab:
load ark1.mat; 
h=ark1cnc;

fprintf(fid,'Subject %s, Base atten=%i:\n',subjID,baseatten);


for i=lists
    fprintf(fid,'List %i:\n',i);
    
    disp(sprintf('Now playing from list %i...',i));
    Nwordtot=0;
    Cwordtot=0;
    AllN=0;
    Alltot=0;
    
    %Load list audio and text files
    Listfile=sprintf('%sMSTB_CNC_Word_list.txt',soundPath);
    fidL=fopen(Listfile);
    listlen=50;
    skipj=(i-1)*(listlen+3)+2;
    SoundFile=sprintf('%sTrack%i.wav',soundPath,i+26);
    coli=2;

%     Y=wavread(SoundFile);
%     figure(7); clf;
%     plot(Y(:,coli)); hold on;
    for j=1:skipj
        fgets(fidL);
    end
    parseN=ParseSet{i};
    
    for j=1:listlen
        set(cr,'String',num2str(j));
        %Read and print out sentence words from list
        Nwords=0;
        Allcorrect=0;
        sent=fgets(fidL);
        [T,R]=strtok(sent,'.');
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
        N2=parseN(j)+40000;
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
        fprintf(fid,'%i %i %i %i %s\n',j,Nwords,Ncorrect,Allcorrect,wcorrectstr);
        Nwordtot=Nwordtot+Nwords;
        Cwordtot=Cwordtot+Ncorrect;
        AllN=AllN+1;
        Alltot=Alltot+Allcorrect;
        %         disp(sprintf('Word %i of List %i: Hit enter to continue',j,i));
        %         pause;
    end
    fclose(fidL);
    perc=Cwordtot/Nwordtot*100;
    percA=Alltot/AllN*100;
    fprintf(fid,'\nTotal correct: %i of %i = %.1f percent Phonemes correct.\n',Cwordtot,Nwordtot,perc);
    disp(sprintf('Total correct: %i of %i = %.1f percent Phonemes correct.\n',Cwordtot,Nwordtot,perc));
    fprintf(fid,'\n');
    fprintf(fid,'Total correct: %i of %i = %.1f percent Words correct.\n',Alltot,AllN,percA);
    disp(sprintf('Total correct: %i of %i = %.1f percent Words correct.\n',Alltot,AllN,percA));
    fprintf(fid,'\n');
end

fclose(fid);
close all;
