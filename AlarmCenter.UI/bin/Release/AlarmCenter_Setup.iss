; -- Example2.iss --
; Same as Example1.iss, but creates its icon in the Programs folder of the
; Start Menu instead of in a subfolder, and also creates a desktop icon.

; SEE THE DOCUMENTATION FOR DETAILS ON CREATING .ISS SCRIPT FILES!

[Setup]
AppName=CIMS100
AppVersion=1.5
DefaultDirName={pf}\Senboll\CIMS100
; Since no icons will be created in "{group}", we don't need the wizard
; to ask for a Start Menu folder name:
DisableProgramGroupPage=yes
UninstallDisplayIcon={app}\CIMS100.exe
OutputDir=userdocs:Inno Setup Examples Output
OutputBaseFilename=CIMS100(Setup)-20160928

[Files]
Source: "CIMS100.exe"; DestDir: "{app}" 
Source: "AxInterop.MSWinsockLib.dll"; DestDir: "{app}"
Source: "CIMS100.exe.config"; DestDir: "{app}"
Source: "CIMSEvent.mdb"; DestDir: "{app}"
Source: "CIMSMain.mdb"; DestDir: "{app}"
Source: "Interop.MSWinsockLib.dll"; DestDir: "{app}"
Source: "map1.bmp"; DestDir: "{app}"
Source: "MSWINSCK.OCX"; DestDir: "{app}"
Source: "PicNode.dll"; DestDir: "{app}"
Source: "TransTool.exe"; DestDir: "{app}"

[Icons]
Name: "{commonprograms}\CIMS100"; Filename: "{app}\CIMS100.exe"
Name: "{commondesktop}\CIMS100"; Filename: "{app}\CIMS100.exe"
