using Windows.Devices.Radios;

// The main class for managing Bluetooth operations.
public class BluetoothManager
{
    // The entry point of the application.
    public static async Task Main(string[] args)
    {
        // Check if the correct command-line arguments are provided.
        if (args.Length == 0 || (args[0].ToLower() != "on" && args[0].ToLower() != "off"))
        {
            Console.WriteLine("Usage: BluetoothSwitcher.exe [on|off]");
            return;
        }

        try
        {
            // Request access to control the radios on the device.
            var accessStatus = await Radio.RequestAccessAsync();
            if (accessStatus != RadioAccessStatus.Allowed)
            {
                Console.WriteLine("Access to radio control is not permitted.");
                return;
            }

            // Get a list of all radios on the device.
            var radios = await Radio.GetRadiosAsync();

            // Find the first radio that is a Bluetooth adapter.
            var bluetoothRadio = radios.FirstOrDefault(radio => radio.Kind == RadioKind.Bluetooth);

            if (bluetoothRadio == null)
            {
                Console.WriteLine("No Bluetooth adapter was found.");
                return;
            }

            // Process the "on" or "off" command.
            string command = args[0].ToLower();
            if (command == "on")
            {
                await bluetoothRadio.SetStateAsync(RadioState.On);
                Console.WriteLine("Bluetooth was turned on successfully.");
            }
            else if (command == "off")
            {
                await bluetoothRadio.SetStateAsync(RadioState.Off);
                Console.WriteLine("Bluetooth was turned off successfully.");
            }
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors.
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}