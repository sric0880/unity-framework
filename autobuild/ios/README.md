1. generate embedded.mobileprovision file here

2. generate entitlements.plist file here. use this cmd:
  ```sh
  /usr/libexec/PlistBuddy -x -c "print :Entitlements " /dev/stdin <<< $(security cms -D -i $PROVISION_FILE) > $ENTITLEMENTS_FILE
  ```
