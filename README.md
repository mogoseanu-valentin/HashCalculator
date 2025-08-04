# HashCalculator

A Windows desktop application built with WPF (.NET 8) for calculating and managing file hash values using SHA-256 and SHA-512 algorithms.

## Features

- **Hash Calculation**: Calculate SHA-256 and SHA-512 hash values for any file
- **File Selection**: Easy file browsing with integrated file dialog
- **Results Management**: View, save, and manage hash calculation results
- **Persistent Storage**: Hash results are saved in JSON format for future reference
- **User-Friendly Interface**: Clean WPF interface with drag-and-drop support

## Screenshots

The application provides an intuitive interface with:
- Hash function selection (SHA-256/SHA-512)
- File browser integration
- Results history panel
- Copy and delete functionality for results

## Requirements

- **Framework**: .NET 8.0 Windows
- **Platform**: Windows (WPF application)
- **Dependencies**: System.Drawing.Common

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/mogoseanu-valentin/HashCalculator.git
   ```

2. Open the solution in Visual Studio or your preferred IDE

3. Build and run the application:
   ```bash
   dotnet build
   dotnet run
   ```

## Usage

1. **Select Hash Algorithm**: Choose between SHA-256 or SHA-512
2. **Choose File**: Click the file selection button or drag-and-drop a file
3. **Calculate Hash**: The hash value will be automatically calculated and displayed
4. **Manage Results**: View previous calculations, copy hash values, or delete entries

## Project Structure

```
HashCalculator/
├── App.xaml                    # Application definition
├── App.xaml.cs                # Application code-behind
├── MainWindow.xaml            # Main window UI definition
├── MainWindow.xaml.cs         # Main window logic
├── HashCalc.cs                # Core hash calculation methods
├── HashFunctions.cs           # Hash algorithm enumeration
├── HashResult.cs              # Hash result data model
├── Data.cs                    # Data management utilities
├── Styles.xaml                # Application styling
├── AppResources.resx          # Application resources
├── hashResults.json           # Persistent storage for results
└── Resources/                 # Application icons and images
    ├── copy.png
    ├── delete.png
    └── pdf.png
```

## Core Components

### HashCalc Class
- `H256(string filePath)`: Calculates SHA-256 hash for a file
- `H512(string filePath)`: Calculates SHA-512 hash for a file

### HashResult Class
- Represents a hash calculation result with file path, hash value, and algorithm type

### HashFunctions Enum
- Defines supported hash algorithms (SHA256, SHA512)

## Technical Details

- **Target Framework**: .NET 8.0-windows
- **UI Framework**: WPF (Windows Presentation Foundation)
- **Hash Algorithms**: SHA-256, SHA-512 (using System.Security.Cryptography)
- **Data Persistence**: JSON serialization for results storage
- **File I/O**: Stream-based file reading for memory efficiency

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## License

This project is open source. Please check the repository for license details.

## Support

For issues, questions, or contributions, please visit the [GitHub repository](https://github.com/mogoseanu-valentin/HashCalculator).
