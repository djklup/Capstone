function fname = GetFolder(path)FoldString='';for i=length(path):-1:1	if path(i)==':'		fname=FoldString;		return;	end	FoldString=[path(i) FoldString];endfname=FoldString;	