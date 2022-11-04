name: Deploy to Lebeda

on:
  workflow_dispatch:
    
    
jobs:
  build:

    runs-on: ubuntu-latest

    steps:
    - uses: actions/checkout@v3
      with: 
        ref: 'gh-pages'
        path: './page'
    - run: sudo apt install ncftp
    - run: ncftpput -R -u '${{ secrets.LEBEDA_FTP_USERNAME }}' -p '${{ secrets.LEBEDA_FTP_PASSWORD }}' lebedahosting.cz www './page'
  
