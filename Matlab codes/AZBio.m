function [perc] = AZBio(subjID,lists,SNR,baseatten)
%Use this function to present AZBio sentences via MATLAB
%e.g. AZBio('test',[1 13],10,30); %for subject 'test', lists 1 and 13,+10
%                                 %  dB SNR, 30 dB base attenuation.
%If only two parameters are entered, defaults for remaining parameters:
%  SNR=90 (quiet) and baseatten=23 (60 dBa).

if nargin<4,
    baseatten=23;   %update this to correct atten for desired SPL
end
if nargin<3,
    SNR=90;
end

disp('This is running the correct AZBio');

soundPath='/SoundFiles/AZBio/';
pathSave=['/Experiments/Data/' subjID '/'];

feval('cd',soundPath);
load AZBioParseSet;

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
PA5_2.SetAtten(baseatten+SNR);   
errorl=PA5_2.GetError();
if length(errorl)~=0
    PA5_2.Display(errorl, 0);
end


flist=dir; 
flist=flist(3:end); 
subjID=GetFolder(subjID);
outFile=fileExistCheck(flist,...
     [subjID '_AZBio_0.txt']);
fid=fopen(outFile,'wt');
disp(sprintf('saved to %s \n',[pathSave outFile]));


cr=status_bar;  % initializing the current run status bar
%loading and executing the "new" GUI derived from matlab:
load ark1.mat; 
h=ark1azb;

fprintf(fid,'Subject %s, Base atten=%i, SNR=%i:\n',subjID,baseatten,SNR);


for i=lists
    fprintf(fid,'List %i:\n',i);
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
%     for j=1:20       
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
    
    disp(sprintf('Now playing from list %i...',i));
    Nwordtot=0;
    Cwordtot=0;
    for j=1:20
       	set(cr,'String',num2str(j));
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
        wcorrectstr='';
        for m=1:12
            if Wcorrect(m)==1
                wcorrectstr=sprintf('%s%s,',wcorrectstr,words{m});
            end
        end
        fprintf(fid,'%i %i %i %s\n',j,Nwords,Ncorrect,wcorrectstr);
        Nwordtot=Nwordtot+Nwords;
        Cwordtot=Cwordtot+Ncorrect;
%         disp(sprintf('Sentence %i of List %i: Hit enter to continue',j,i));
%         pause;
    end
    fclose(fidL);
    perc=Cwordtot/Nwordtot*100;
    fprintf(fid,'Total correct: %i of %i = %.1f percent correct.\n',Cwordtot,Nwordtot,perc);
    disp(sprintf('Total correct: %i of %i = %.1f percent correct.\n',Cwordtot,Nwordtot,perc));
    fprintf(fid,'\n');
end

fclose(fid);
close all;
