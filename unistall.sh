#!/bin/bash
    systemctl stop 3xui-multi-protocol
    systemctl disable 3xui-multi-protocol
    rm -f /etc/systemd/system/3xui-multi-protocol.service 
    systemctl daemon-reload
    systemctl reset-failed
    rm -rf /etc/3xui-multi-protocol 
    rm -rf 3xui-multi-protocol/3xui-multi-protocol 
    sudo apt-get purge dotnet-sdk-7.0
     sudo rm -f /etc/apt/sources.list.d/microsoft-prod.list && sudo apt update

    echo "Uninstalled Successfully!"

