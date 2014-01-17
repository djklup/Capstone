function []=CNCfix(subjid,run_number);

%Append updated scores to CNC file if errors corrected

eval(sprintf('cd c:/Experiments/Data/%s',subjid));
fname=sprintf('%s_CNC_%i.txt',subjid,run_number);
fid=fopen(fname,'r+');

temp=fgetl(fid);
list=fgetl(fid);
listset{1}=list;
n=1;
while list~=-1 & length(list)>1
    for i=1:50
        temp=fgetl(fid);
        [T,R]=strtok(temp,' ');
        num(n,i)=str2num(T);
        [T,R]=strtok(R,' ');
        maxp(n,i)=str2num(T);
        [T,R]=strtok(R,' ');
        p(n,i)=str2num(T);
        [T,R]=strtok(R,' ');
        w(n,i)=str2num(T);
    end
    pcorrect(n)=sum(p(n,:))/sum(maxp(n,:))*100;
    wcorrect(n)=sum(w(n,:))/50*100;
    
    disp(sprintf('New total correct for %s %i of %i = %.1f percent Phonemes correct.\n',list,sum(p(n,:)),sum(maxp(n,:)),pcorrect(n)));
    disp(sprintf('New total correct for %s %i of 50 = %.1f percent words correct.\n',list,sum(w(n,:)),wcorrect(n)));
    
    for i=1:5
        temp=fgetl(fid);
    end
    list=fgetl(fid);
    n=n+1;
    listset{n}=list;
end
fclose(fid);

fid=fopen(fname,'a+');
fprintf(fid,'********************************************************************\r\n');
for i=1:n-1
    list=listset{i};
    fprintf(fid,'\r\n');
    fprintf(fid,'New total correct for %s %i of %i = %.1f percent Phonemes correct.\r\n',list,sum(p(i,:)),sum(maxp(i,:)),pcorrect(i));
    fprintf(fid,'\r\n');
    fprintf(fid,'New total correct for %s %i of 50 = %.1f percent words correct.\r\n',list,sum(w(i,:)),wcorrect(i));
    fprintf(fid,'\r\n');
end
% isdone=0;
% while (isdone==0)
%     temp=fgetl(fid);
%     temp=fgetl(fid);
%     [T,R]=strtok(temp,' ');
%     [T]=strtok(R,':');
%     list=str2num(T);
%     
%     
%     
%     isdonestr=input('Done? N=no,Y=yes','s');
%     if strcmp(isdonestr,'Y') | strcmp(isdonestr,'y')
%         isdone=1;
%     end
% end


fclose(fid);

return
