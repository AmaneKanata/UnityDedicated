@echo off
SETLOCAL

:: Define paths
set ROOT_DIR=%~dp0
set PROTO_PATH=%ROOT_DIR%src\ProtoFiles
set TEMPLATE_PATH=%ROOT_DIR%src\Templates
set OUTPUT_PATH=%ROOT_DIR%output
set PROTOC=%ROOT_DIR%src\protoc.exe
set GENERATOR=%ROOT_DIR%src\Generator.exe

:: Ensure output directory exists
if not exist %OUTPUT_PATH% mkdir %OUTPUT_PATH%

:: Compile .proto files to C# source code
echo Compiling .proto files to C#...
for %%f in (%PROTO_PATH%\*.proto) do (
    echo Processing %%~nxf
    %PROTOC% -I=%PROTO_PATH% --csharp_out=%OUTPUT_PATH% %%f
)

:: Run the Packet Generator
echo Running Packet Generator...
%GENERATOR% --proto_path "%PROTO_PATH%" --template_path "%TEMPLATE_PATH%" --output_path "%OUTPUT_PATH%\Server" --recv "C_" --send "S_"
%GENERATOR% --proto_path "%PROTO_PATH%" --template_path "%TEMPLATE_PATH%" --output_path "%OUTPUT_PATH%\Client" --recv "S_" --send "C_"

echo All tasks completed successfully.
ENDLOCAL
