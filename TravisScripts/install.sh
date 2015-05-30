#! /bin/sh

echo 'Downloading from http://netstorage.unity3d.com/unity/0b02744d4013/MacEditorInstaller/Unity-5.0.2f1.pkg'
curl -O http://netstorage.unity3d.com/unity/0b02744d4013/MacEditorInstaller/Unity-5.0.2f1.pkg

echo 'Installing Unity.pkg'
sudo installer -dumplog -package Unity-5.0.2f1.pkg -target /