%Calibration script for MSTB CD - AZbio or CNC
%This should calibrate to 60 dBA - find necessary PA5 attenuation for this
%desired calibration level

function []=Calib_MSTB();

[y,fs]=wavread('C:/SoundFiles/CNC/MSTB_CD/Track38.wav');    %load 2-channel noise
%[y,fs]=wavread('C:/SoundFiles/AZBio/10TalkerBabbleforCalib.wav');    %load 2-channel noise

%for i=1:10
    sound(y,fs);
%end
return