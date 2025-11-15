# 301.3 Development

# Overview for SERVER API

This is the backend API for the Hospital Management System.
The API provides secure endpoints for:

Patient authentication

User & patient data retrieval
Appointment management

Patient data exporting

Secure password handling with PBKDF2

AES-256 encryption for sensitive values

The server is built with ASP.NET Core, Entity Framework Core, and SQLite.

Features
Authentication

Session-based login flow

PBKDF2 password hashing

Secure token/session key generation

Patient / User Management:
Retrieve user details
Access patient profile

Export patient records:
GET /api/Auth/patient/export/{userId}

Appointment System
Create, read, update, delete appointments
Filtered retrieval via PatientID

Security
Fully parameterized EF Core (SQL-Injection safe)
AES-256 encryption used in combination with PBKDF2 key derivation
Automatic HTTPS redirection (recommended in production)

Requires:
.NET 8 SDK
SQLite
(Optional) Raspberry Pi for deployment

How to start:
Run "HospitalApp" on external machine on local network

bash:
~ dotnet run

api starts on:
https://<HostIP>:5001
or
https://<user@host>:5001


# Overview for SERVER API
API Configuration

In <301.3 Development\Scripts\Session\SessionManager.cs>
Api = new ApiClient("https://<raspberrypi.local>:5001/api/");

set "raspberrypi.local" to host ip if user@host do not match