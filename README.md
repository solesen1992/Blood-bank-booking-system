# Blood bank booking system

Blood bank booking systemet is made for DonateBlood.Now (not a real company). This documentation describes the purpose and functionality of our system designed for creating and managing bookings. The project is a distributed system with client-server achitecture and is made with different components: A web app and a desktop app that is both connected to a REST API and database.

The project uses concurrency, ACID principles etc. It's made in C# and MS SQL.

This project is made on the 3rd semester of my education in computer science as a part of a group project in the fall of 2024.

# Screenshot of web app
<img width="800" alt="Image" src="https://github.com/user-attachments/assets/b13ab693-f2c9-4fb5-aa8b-c452660f262e" />

# Documentation
## Functionality
This project is a blood bank booking system designed to facilitate donor management and appointment scheduling. The system is built as a distributed architecture consisting of the following components:

- <b>Web Application (ASP.NET Core MVC):</b> Allows donors to register, book appointments, and manage their information.
- <b>Desktop Application (WinForms):</b> Enables staff to manage donor data and oversee appointment scheduling.
- <b>REST API:</b> Serves as the backend, handling business logic and database communication.
- <b>Database (MS SQL Server):</b> Stores donor information, appointments, and related data, ensuring data integrity and consistency.

## Dependency Injection
Dependency Injection (DI) is used to manage dependencies across the application efficiently. Instead of creating objects manually in each class, dependencies are injected at runtime, improving flexibility and maintainability.

By centralizing dependency management, we ensure that controllers, services, and repositories are loosely coupled, making the system easier to modify and extend. This was essential for our development approach as it allowed multiple team members to work on different components independently without causing conflicts in object instantiation.

## Data Transfer Object (DTO)
DTOs are used to transfer data between the API and clients without exposing internal data models. This abstraction improves security by ensuring that only necessary fields are exposed and sensitive information is not leaked.

In our project, DTOs played a crucial role in data validation and consistency. For example, in the donor registration and appointment booking process, DTOs ensured that only required fields such as name and blood type were transmitted, preventing unnecessary data from being exposed.

## Dapper
To efficiently manage database interactions, we utilized Dapper, a lightweight ORM (Object-Relational Mapper) for .NET. Dapper was chosen over alternatives like Entity Framework due to its high performance and minimal overhead. Unlike traditional ORMs that generate SQL queries dynamically, Dapper allows us to write raw SQL queries while still benefiting from automatic object mapping.

In our project, Dapper is used in the API layer to interact with the MS SQL Server database, particularly for donor registration, appointment booking, and retrieval of donor information. The main advantage of using Dapper is its speed and flexibility, as it does not introduce significant performance overhead. However, the downside is that it requires more manual query writing, which can lead to potential SQL injection risks if not handled properly. To mitigate this, we ensured the use of parameterized queries for security.

## Use of JSON
In our system, communication between clients and the API requires a common data format since they are different systems. We use JSON (JavaScript Object Notation) as the standardized text format that enables data exchange over HTTPS across different technologies.
- <b>Data Serialization and Deserialization:</b> JSON is used to serialize data before transmitting it over HTTP requests and deserialize it upon reception. This ensures that data transferred between the web client, desktop client, and API remains structured and easily interpretable.

When our API receives a JSON request from a client, middleware ensures the following operations:
- <b>Deserialization:</b> JSON data from the client is converted into a .NET object, which the API controllers can work with.
- <b>Serialization:</b> Once the response is processed, the .NET object is converted back into JSON format and sent back as an HTTP response.
In ASP.NET Core, serialization and deserialization happen as part of the middleware process, using the built-in System.Text.Json.

Different areas where JSON processing happens in our system:
- <b>Browser to MVC:</b> The browser sends an HTTPS request, and the MVC application returns HTML, CSS, and JavaScript to display the data. No serialization or deserialization occurs here because data is presented directly.
- <b>Web app/Desktop to API:</b> The client (Web app or Desktop app) sends JSON data to the API as part of an HTTPS request. For example, when a user registers as a donor in the web app, their information is serialized into JSON before being sent through a POST request.
- <b>API to Database:</b> The API communicates with the database via TCP/IP, converting structured database results into objects. These objects are then serialized into JSON before being sent back to the client.
By using JSON, we ensure that all systems within our architecture can efficiently communicate while maintaining a lightweight and structured data format.

## Network Considerations
Given that our system operates as a distributed architecture, network communication plays a crucial role in ensuring seamless interaction between clients and the API.

- <b>HTTPS:</b> Our API endpoints communicate over HTTPS to ensure secure data transmission and prevent attacks like man-in-the-middle (MITM).
- <b>RESTful Communication:</b> The API follows REST principles, using standard HTTP methods (GET, POST, PUT, DELETE) for interacting with resources. This approach ensures a scalable and easy-to-maintain system.

## Network Architecture
Our system is designed as a distributed system where different components communicate over a network using various protocols operating on different layers of the TCP/IP model.

### Network Access Layer
This layer is responsible for transmitting data between devices using wired or wireless connections.
- <b>Client and API:</b> Since communication between the API and client occurs locally (on localhost), no actual physical transmission takes place. Instead, the operating system simulates the transmission.
- <b>API and Database:</b> Data is encapsulated into Ethernet frames and transmitted over wired or wireless networks. The frames include MAC addresses to ensure that data reaches the correct machine.

### Network Layer
The network layer uses IP addresses to ensure that packets reach the correct destination.
- <b>Client and API:</b> Data is transmitted locally through the loopback address (127.0.0.1).
- <b>API and Database:</b> To communicate with the database server (hildur.ucn.dk), the DNS system resolves the domain name to an IP address, and data is then sent to this IP.

### Transport Layer
The transport layer ensures reliable data transmission using TCP (Transmission Control Protocol).
- <b>Segmentation & Port Numbers:</b> TCP divides data into smaller packets and assigns port numbers to ensure they reach the correct service (e.g., port 443 for HTTPS).
- <b>Reliability:</b> TCP guarantees error detection, retransmission of lost packets, and ordering of received packets.

### Application Layer
The application layer processes and interprets the byte stream, handling protocols like HTTPS and Tabular Data Stream (TDS).
- <b>Client and API:</b> The client formats user input into JSON and sends it to the API over HTTPS. The API interprets the HTTP requests (GET, POST) and sends responses accordingly.
- <b>API and Database:</b> The API deserializes JSON data into .NET objects, then converts these objects into SQL queries using Dapper. These queries are sent to the database via TDS (Tabular Data Stream protocol), which is specific to MS SQL Server. The database processes the query and returns the result to the API.

By implementing this network architecture, we ensure that data is transmitted efficiently between different components while maintaining security and reliability.

## Concurrency Handling
Since multiple users may attempt to book the same appointment, concurrency control is implemented using Optimistic Concurrency Control (OCC). OCC assumes that conflicts are rare and allows transactions to proceed without locking records. Before committing changes, the system checks whether the data has been modified by another transaction.

Our project required robust concurrency control due to the real-time nature of appointment booking. Without it, multiple users could select the same time slot simultaneously, resulting in double bookings. We used OCC to detect conflicts before committing transactions, which prevented inconsistencies in appointment scheduling.

The advantage of OCC is that it enhances performance, as it does not require long-held locks that can slow down operations. However, OCC requires additional logic to detect conflicts and retry operations, which can lead to a more complex implementation. We handled this by implementing an overlap checker that verifies appointment availability before allowing a new booking to be committed.

## ACID Principles
To ensure database consistency, transactions follow the ACID principles:

- <b>Atomicity:</b> Transactions either complete fully or not at all, preventing partial updates. This is critical for our system to ensure that appointment bookings are either fully confirmed or discarded, avoiding half-processed transactions.
- <b>Consistency:</b> Ensures that the database transitions from one valid state to another. In our project, this is maintained by enforcing constraints that prevent invalid or duplicate appointment entries.
- <b>Isolation:</b> Ensures that concurrent transactions do not interfere with each other, preventing race conditions. We used a Read Uncommitted isolation level.
- <b>Durability:</b> Once a transaction is committed, it remains in the database even in case of system failures. This guarantees that confirmed appointments are not lost in the event of server crashes or network failures.

## Security Considerations
Security is critical for handling sensitive donor data. The system haven't implemented all the security measures but we're had different considerations.
- <b>Authentication & Authorization (not implemented):</b> JWT-based authentication ensures that only authorized users can access sensitive endpoints. This method was chosen for its stateless nature, reducing server load while providing secure token-based authentication.
- <b>HTTPS Encryption:</b> Encrypting all client-server communication protects against man-in-the-middle attacks but requires SSL/TLS certificates and additional configuration. In our project, HTTPS was enforced for all API requests to prevent data interception.
- <b>SQL Injection Prevention:</b> Parameterized queries prevent attackers from injecting malicious SQL commands. This was crucial for protecting our database from unauthorized access and manipulation.
- <b>Hashing and Salting (not implemented):</b> Passwords should be stored securely using hashing and salting techniques, preventing attackers from recovering plaintext passwords. We planned for a future integration with MitID authentication, ensuring compliance with secure authentication practices.

Each of these measures enhances security but also introduces implementation complexity. However, given the sensitive nature of medical data, these security practices were essential in maintaining user trust and regulatory compliance.

# Conclusion
This documentation outlines the key architectural and implementation details of the system, including why we use different techniques, their advantages and drawbacks, and how they are implemented in the application. By leveraging best practices in dependency injection, data transfer, concurrency control, ACID compliance, and security, we ensure a robust and scalable system for managing blood donations efficiently. Our choices were guided by the need for maintainability, performance, and security, ensuring that the system can be extended and improved in future iterations.
