#! /bin/sh
 
# Example build script for Unity3D project. See the entire example: https://github.com/JonathanPorta/ci-build
 
# Change this the name of your project. This will be the name of the final executables as well.
project="Pong-X"
 
echo "Attempting to build $project for Windows"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/unity.log \
  -projectPath "$(pwd)/$project" \
  -buildWindowsPlayer "$(pwd)/Build/$project-win/$project.exe" \
  -quit
 
echo "Attempting to build $project for OS X"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/unity.log \
  -projectPath "$(pwd)/$project" \
  -buildOSXUniversalPlayer "$(pwd)/Build/$project-osx/$project.app" \
  -quit
 
echo "Attempting to build $project for Linux"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/unity.log \
  -projectPath "$(pwd)/$project" \
  -buildLinuxUniversalPlayer "$(pwd)/Build/$project-lin/$project" \
  -quit
 
echo 'Logs from build'
cat $(pwd)/unity.log

echo 'Packing the build files to zip files'
cd $(pwd)/Build/
echo 'Packing Windows build...'
rm ./$project-win/*.pdb
zip -r $project-win.zip ./$project-win
echo 'Packing OSX build...'
zip -r $project-osx.zip ./$project-osx
echo 'Packing Linux build...'
zip -r $project-x-lin.zip ./$project-lin
echo 'Zip files are ready to deploy.'
cd ..