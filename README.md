# ☁️ AWS S3 Management App

## 🌟 Overview

This project is a WPF application built to interact programmatically with AWS S3. 
It enables users to manage S3 buckets and objects efficiently with a simple and intuitive GUI. 
The application demonstrates bucket-level and object-level operations, providing a hands-on approach to understanding AWS S3 and IAM services.

## 🛠️ Features
1. Graphical User Interface (GUI)

    Main Window: Serves as the entry point to bucket and object operations.
    Bucket-Level Operations:
        View, create, and delete S3 buckets.
        Updates the display dynamically with every action.
    Object-Level Operations:
        View objects in selected buckets, upload, and download objects.

2. Bucket-Level Operations

    Retrieve and list all buckets in your AWS account.
    Create new buckets and dynamically update the display.
    Delete buckets (empties the bucket if necessary before deletion).

3. Object-Level Operations

    List all objects in a selected bucket.
    Upload objects to a selected bucket and view them instantly.
    Download objects locally with a simple click.

## 🧰 Technical Stack

    Languages: C#, XAML
    Framework: .NET WPF
    Cloud Services: AWS S3, AWS IAM
    AWS SDK: AWS SDK for .NET

## 🎯 Project Goals

    Develop a WPF application to interact with AWS S3.
    Implement bucket-level operations:
        List, create, and delete buckets.
    Implement object-level operations:
        View, upload, and download objects.
    Ensure code quality, efficiency, and maintainability.

## 📋 Instructions

1. AWS Setup

    IAM Credentials:
        Create an AWS IAM user with programmatic access and S3 permissions.
        Save the Access Key ID and Secret Access Key to the appsettings.json.

    S3 Buckets:
        Use the app to create and manage buckets, or prepare existing buckets in your AWS account.

## 🖼️ Screenshots

Main Window
---
![Screenshot 2024-12-04 163145](https://github.com/user-attachments/assets/00482543-df24-4441-afdc-6ae09700fd1f)

Bucket-Level Operations
---
![Screenshot 2024-12-04 163159](https://github.com/user-attachments/assets/243fb55c-3d84-4d8a-8100-18b4aff1eb12)
![Screenshot 2024-12-04 163256](https://github.com/user-attachments/assets/0614f988-cf63-47e8-bec1-79a495aa7624)
![Screenshot 2024-12-04 163306](https://github.com/user-attachments/assets/5b7a8d6c-d92d-4429-828d-740481454a3f)
![Screenshot 2024-12-04 163312](https://github.com/user-attachments/assets/ab24168f-9d33-4dd5-83b5-2f048524f5de)
![Screenshot 2024-12-04 163319](https://github.com/user-attachments/assets/af5ef94a-1ba1-4c1f-af56-5fda8b1465f0)
![Screenshot 2024-12-04 163327](https://github.com/user-attachments/assets/e95aec25-2440-4bf3-a113-32eba10a5bd2)
![Screenshot 2024-12-04 163332](https://github.com/user-attachments/assets/d9b54f66-00c5-413f-bea1-20f3d9887a91)

Object-Level Operations
---
![Screenshot 2024-12-04 163342](https://github.com/user-attachments/assets/9d6a3f8b-48b0-4474-b056-2818d52e5871)
![Screenshot 2024-12-04 163349](https://github.com/user-attachments/assets/08c0ef53-17ac-40df-963d-bab21c868bb9)
![Screenshot 2024-12-04 163404](https://github.com/user-attachments/assets/a8925a31-8a2b-43e0-aeae-54db7bbd6519)
![Screenshot 2024-12-04 163412](https://github.com/user-attachments/assets/c3153a85-16c7-49a3-be4e-a7e8036a7eeb)
![Screenshot 2024-12-04 163417](https://github.com/user-attachments/assets/339b4330-43e9-437c-9096-9804fb86632c)
![Screenshot 2024-12-04 163426](https://github.com/user-attachments/assets/04e0f3df-2217-41d9-bcfa-d9912de3389c)
![Screenshot 2024-12-04 163434](https://github.com/user-attachments/assets/f83517c1-dc0b-40e9-a4cd-47f17966ff45)

## 📜 License

This project is licensed under the MIT License.
