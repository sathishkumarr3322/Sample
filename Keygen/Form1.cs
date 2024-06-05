using System;
using System.IO;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CopyFileToTargetPath()
        {
            // Get the current directory where the application is running
            string currentDir = Directory.GetCurrentDirectory();

            // Source file path
            string sourceFile = Path.Combine(currentDir, "ApplicationConfig_Col.binary");

            // Check if the source file exists
            if (!File.Exists(sourceFile))
            {
                MessageBox.Show($"Source file not found.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1); // Exit with an error code
            }

            // Specify the name of the environment variable containing the target directory path
            string environmentVariableName = "MSOAppPath"; // Replace this with the name of your environment variable

            // Retrieve the value of the environment variable (target directory path)
            string targetDirectory = Environment.GetEnvironmentVariable(environmentVariableName);

            // Check if the environment variable is set
            if (string.IsNullOrEmpty(targetDirectory))
            {
                MessageBox.Show($"Environment variable is not set.", "Environment Variable Not Set", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1); // Exit with an error code
            }

            // Subdirectories specific to your application
            string appPath = Path.Combine(targetDirectory, "User_Templates_Config");

            try
            {
                // Create the target file path by combining the target directory and the file name
                string fileName = Path.GetFileName(sourceFile);
                string targetFilePath = Path.Combine(appPath, fileName);

                // Check if the file already exists in the target path
                if (File.Exists(targetFilePath))
                {
                    MessageBox.Show($"File already exists in the target folder and cannot be copied.", "File Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Environment.Exit(1); // Exit with an error code
                }

                // Copy the file from the source path to the target path
                File.Copy(sourceFile, targetFilePath);

                Console.WriteLine("File copied successfully.");

                // Disable the form after completing the task
                this.Enabled = false;

                // Stop the execution of the program
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Call the method to copy the file to the target path when the form is loaded
            CopyFileToTargetPath();
        }
    }
}
