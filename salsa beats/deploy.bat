set OutDir=t:\sage\exp\salsa beats
chdir $(ProjectDir)
rd "%OutDir%" /s /q
mkdir "%OutDir%"
mkdir "%OutDir%\bin"
mkdir "%OutDir%\res"
copy  "$(TargetPath)" "%OutDir%\bin\" /y
@echo .mp3 > excludes.txt
xcopy "$(ProjectDir)res" "%OutDir%\res" /s /q /y /exclude:excludes.txt
del excludes.txt
