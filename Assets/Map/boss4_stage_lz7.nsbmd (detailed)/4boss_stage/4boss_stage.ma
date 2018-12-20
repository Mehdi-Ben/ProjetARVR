//Maya ASCII 3.0 scene
//Created by MKDS Course Modifier

requires maya "3.0";
currentUnit -l centimeter -a degree -t film;
createNode joint -n "_4boss_stage" -p "_4boss_stage";
setAttr ".t" -type "double3" 0 0 0;
setAttr ".r" -type "double3" 0 0 0;
setAttr ".s" -type "double3" 1 1 1;
createNode joint -n "bubble" -p "_4boss_stage";
setAttr ".t" -type "double3" 0 -1.870113 0;
setAttr ".r" -type "double3" 0 0 0;
setAttr ".s" -type "double3" 0.06282425 0.06604004 0.06282425;
createNode joint -n "enkei" -p "_4boss_stage";
setAttr ".t" -type "double3" 0 0 0;
setAttr ".r" -type "double3" 0 0 0;
setAttr ".s" -type "double3" 1 1 1;
createNode joint -n "stage" -p "_4boss_stage";
setAttr ".t" -type "double3" 0 0 0;
setAttr ".r" -type "double3" 0 0 0;
setAttr ".s" -type "double3" 1 1 1;
createNode joint -n "weak_1" -p "_4boss_stage";
setAttr ".t" -type "double3" 1.406254 -0.4933128 0;
setAttr ".r" -type "double3" 0 0 0;
setAttr ".s" -type "double3" 1 1 1;
createNode joint -n "weak_2" -p "_4boss_stage";
setAttr ".t" -type "double3" -1.406254 -1.380878 0;
setAttr ".r" -type "double3" 0 0 0;
setAttr ".s" -type "double3" 1 1 1;
