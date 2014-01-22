function [perc] = AZBio_practice(SNR,baseatten)
%Use this function to present practice AZBio sentences via MATLAB
%First 5 sentences of List 1 are used.
%e.g. AZBio_practice(10,30); %for +10 dB SNR, 30 dB base attenuation.
%First parameter is SNR (optional, default is 90 for quiet
%Second parameter is base attenuation (optional,default is 23 dB attenuation, equiv to 60 dBa)

if nargin<1,
    SNR=90;
end
if nargin<2,
    baseatten=23;   %update this to correct atten for desired SPL
end

soundPath='/SoundFiles/AZBio/';

feval('cd',soundPath);
load AZBioParseSet;

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
PA5_2.SetAtten(baseatten+SNR);   
errorl=PA5_2.GetError();
if length(errorl)~=0
    PA5_2.Display(errorl, 0);
end


cr=status_bar;  % initializing the current run status bar
%loading and executing the "new" GUI derived from matlab:
load ark1.mat; 
h=ark1azb;

for i=1
    %Read list text file
    Listfile=sprintf('%sList%i.txt',soundPath,i);
    fidL=fopen(Listfile);
    
    %Load list audio file
    SoundFile=sprintf('%sList%i.wav',soundPath,i);
%     [Y,Fs]=wavread(SoundFile);
%     Y=Y(:,1);
% %    disp('Wait 5 seconds for program to parse sentences...');
%     Y=envelope(Y);
%     %Parse file by gaps in audio
%     parseN(1)=1;
%     k=1;
%     for j=1:5       
%         while (Y(k)<0.1)
%             k=k+1;
%         end
%         while (Y(k)>1e-5)
%             k=k+1;
%         end
%         k=k+10000;  %delay offset of stimulus by 10000/44100~1/4 sec 
%         parseN(j+1)=k;
%     end
%     clear Yenv;
    parseN=ParseSet{i};

    disp('Now playing...');
    Nwordtot=0;
    Cwordtot=0;
    for j=1:5
        %Read and print out sentence words from list
        fgets(fidL);
        sent=fgets(fidL);
        Nwords=str2num(fgets(fidL));
        for k=1:12
            [T,sent]=strtok(sent);
             words{k}=T;
             h1 = uicontrol('Parent',h, ...
                'Units','points', ...
                'BackgroundColor',red, ...
                'Callback',sprintf(' shp=%i; ',k),  ...
                'FontSize',20, ...
                'Position',buttonazb(k).position, ...
                'String',words{k}, ...
                'Tag','Pushbutton1');
        end
        h1 = uicontrol('Parent',h, ...
            'Units','points', ...
            'BackgroundColor',red, ...
            'Callback',' shp=14; ',  ...
            'FontSize',24, ...
            'Position',buttonazb(14).position, ...
            'String','Done', ...
            'Tag','Pushbutton1');

        pause(0.5);
        %Play sentence
        N1=parseN(j)+140000; %skip 90000/44100~2 sec of extra noise between
                         % sentences (increase to decrease length of noise before sentence start)
        N2=parseN(j+1)-1;
        [Y,Fs]=wavread(SoundFile,[N1,N2]);
        wavplay(Y,Fs);
        %Get words correct from experimenter and save to file
        Wcorrect=zeros(1,12);
        shpdone=0;
        while (shpdone==0)
            waitbutton;
            answer=shp;
            if answer==14
                shpdone=1;
            elseif answer<=Nwords
                if Wcorrect(answer)==0  %change button color to blue for correct
                    h1 = uicontrol('Parent',h, ...
                        'Units','points', ...
                        'BackgroundColor',blue, ...
                        'Callback',sprintf(' shp=%i; ',answer),  ...
                        'FontSize',20, ...
                        'Position',buttonazb(answer).position, ...
                        'String',words{answer}, ...
                        'Tag','Pushbutton1');
                    Wcorrect(answer)=1;
                else Wcorrect(answer)==1  %change button color back to red for incorrect
                    h1 = uicontrol('Parent',h, ...
                        'Units','points', ...
                        'BackgroundColor',red, ...
                        'Callback',sprintf(' shp=%i; ',answer),  ...
                        'FontSize',20, ...
                        'Position',buttonazb(answer).position, ...
                        'String',words{answer}, ...
                        'Tag','Pushbutton1');
                    Wcorrect(answer)=0;
                end
            elseif answer==13
                 for ai=1:Nwords
                    if Wcorrect(ai)==0
                        h1 = uicontrol('Parent',h, ...
                            'Units','points', ...
                            'BackgroundColor',blue, ...
                            'Callback',sprintf(' shp=%i; ',ai),  ...
                            'FontSize',20, ...
                            'Position',buttonazb(ai).position, ...
                            'String',words{ai}, ...
                            'Tag','Pushbutton1');
                        Wcorrect(ai)=1;
                    else Wcorrect(ai)==1  %change button color back to red for incorrect
                        h1 = uicontrol('Parent',h, ...
                            'Units','points', ...
                            'BackgroundColor',red, ...
                            'Callback',sprintf(' shp=%i; ',ai),  ...
                            'FontSize',20, ...
                            'Position',buttonazb(ai).position, ...
                            'String',words{ai}, ...
                            'Tag','Pushbutton1');
                        Wcorrect(ai)=0;
                    end
                 end
            elseif answer==15
                 wavplay(Y,Fs);
            end
            Ncorrect=sum(Wcorrect);
            Ncorrectstr=sprintf('N=%i',Ncorrect);
            h1 = uicontrol('Parent',h, ...
                'Units','points', ...
                'BackgroundColor',red, ...
                'Callback',' shp=14; ',  ...
                'FontSize',24, ...
                'Position',buttonazb(14).position, ...
                'String',Ncorrectstr, ...
                'Tag','Pushbutton1');
        end
        Nwordtot=Nwordtot+Nwords;
        Cwordtot=Cwordtot+Ncorrect;
    end
    fclose(fidL);
    perc=Cwordtot/Nwordtot*100;
    disp(sprintf('Total correct: %i of %i = %.1f percent correct.\n',Cwordtot,Nwordtot,perc));
end

close all;
