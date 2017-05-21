1. you need to first generate a private key using keytool. For example:

  ```sh
  keytool -genkey -v -keystore keystore -keyalg RSA -keysize 2048 -validity 10000 -alias my-alias
  ```
  change then name `keystore` in config file to your name

2. change the alias name in config file

  ```sh
  export ALIAS='my-alias'
  ```

3. change the password of your keystore in config file

  ```sh
  export KEYSTORE_PWD=123456
  ```

4. change the password of your alias in config file

  ```sh
  export KEY_PWD=123456
  ```
