%startup.m file

load ark1.mat;

warning('off','MATLAB:dispatcher:InexactCaseMatch');

disp('Notes:');
disp('1) ark1.mat has been loaded during startup ...');
disp('2) shortpre has been simplified: can type "shortpre(64,''test'');"');