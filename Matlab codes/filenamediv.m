function [y1,y2]=filenamediv(fname);%function [y1,y2]=filenamediv(fname);%split a string of a file name into body and extension.whereDot=findstr(fname,'.');if isempty(whereDot), y1=fname; y2=[];else   numDot=length(whereDot);   whereDot=whereDot(numDot);   y1=fname(1:whereDot-1);   y2=fname(whereDot+1:length(fname));end