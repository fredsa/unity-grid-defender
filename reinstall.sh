#!/bin/bash

set -ue

MANIFEST_PATH=Assets/Plugins/Android/AndroidManifest.xml

activity=com.unity3d.player.UnityPlayerActivity
root=$( dirname $0 )

# Determine APK name
if [ $# -ge 1 ]
then
  apk=$1
else
  # Look for APKs
  apk=$( ls -1 "$root"/*.apk )
  apk_count=$(( $( echo "$apk" | wc -l ) ))
  if [ $apk_count -ne 1 ]
  then
    echo "ERROR: Found $apk_count APKs but expecting exactly 1" 1>&2
    ls -l $apk 1>&2
    exit 1
  fi
fi

# Determine Android package name
if [ $# -ge 2 ]
then
  pkg=$2
else
  pkg=$( basename $apk | sed -e 's/.apk$//' )
fi

# Determine overriding Android activity name from AndroidManifest.xml
if [ -f "$MANIFEST_PATH" ]
then
  echo
  echo "Extracting activity name from $MANIFEST_PATH …"
  activity=$( grep '<activity android:name="' "$MANIFEST_PATH" | cut -d '"' -f 2 )
fi


uninstall_pkg()
{
  echo
  echo "Uninstalling $1 …"
  adb shell pm uninstall $1 || true
}

install_pkg()
{
  echo
  echo "Installing $1 …"
  adb install -r -g --user 0 $1
}

# Begin actual un/re-install and launch

install_pkg $apk ||
(
  uninstall_pkg $pkg \
   && install_pkg $apk
)

echo
echo "Launching $pkg/$activity …"
adb shell am start -n $pkg/$activity
