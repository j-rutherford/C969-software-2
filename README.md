# Appointment Scheduling Application

## Overview

This project is an Appointment Scheduling Application developed as part of a course at Western Governors University (WGU). The application is designed to manage customer records and appointments for a business environment, allowing users (consultants) to create, view, update, and delete customer and appointment information. The application provides features such as data validation, multi-language support, and user authentication.

## Features

- **Customer Management:** Add, update, delete, and view customers with complete address information (country, city, address).
- **Appointment Management:** Schedule appointments with constraints on business hours (9 AM to 5 PM EST) and prevent overlapping appointments for users.
- **User Authentication:** Secure login system that tracks successful and failed login attempts.
- **Multi-Language Support:** The application supports English and Italian, with dynamic language switching based on the user's environment settings.
- **Reports:**
  - **Monthly Appointment Type Report:** Displays the number of each type of appointment by month.
  - **Consultant Schedule Report:** View individual schedules for consultants with the ability to filter appointments.
  - **Customer Activity Report:** Summarizes customer engagement and appointment history.
- **Notifications:** Alerts users if they have appointments scheduled within the next 15 minutes upon login.
- **Data Integrity:** Ensures data consistency across related tables (customers, appointments, users) with proper handling of foreign keys.

## Technical Details

- **Platform:** .NET Framework 4.7.2, Windows Forms
- **Database:** MySQL with ADO.NET for data access
- **Language:** C#
- **IDE:** Visual Studio 2019

## Prerequisites

- **MySQL Server:** Ensure that MySQL Server is installed and running.
- **Visual Studio 2019:** Required for building and running the project.

## Installation

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/yourusername/appointment-scheduling-app.git
