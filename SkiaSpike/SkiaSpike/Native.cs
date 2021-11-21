using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SkiaSpike
{

    public class Kernel32
    {
        private const string kernel32 = "kernel32.dll";

        static Kernel32()
        {
            CurrentModuleHandle = Kernel32.GetModuleHandle(null);
            if (CurrentModuleHandle == IntPtr.Zero)
            {
                throw new Exception("Could not get module handle.");
            }
        }

        public static IntPtr CurrentModuleHandle { get; }

        [DllImport(kernel32, CallingConvention = CallingConvention.Winapi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPTStr)] string lpModuleName);
    }

    [Flags]
    public enum WindowStyles : uint
    {
        WS_BORDER = 0x800000,
        WS_CAPTION = 0xc00000,
        WS_CHILD = 0x40000000,
        WS_CLIPCHILDREN = 0x2000000,
        WS_CLIPSIBLINGS = 0x4000000,
        WS_DISABLED = 0x8000000,
        WS_DLGFRAME = 0x400000,
        WS_GROUP = 0x20000,
        WS_HSCROLL = 0x100000,
        WS_MAXIMIZE = 0x1000000,
        WS_MAXIMIZEBOX = 0x10000,
        WS_MINIMIZE = 0x20000000,
        WS_MINIMIZEBOX = 0x20000,
        WS_OVERLAPPED = 0x0,
        WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_SIZEFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
        WS_POPUP = 0x80000000u,
        WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,
        WS_SIZEFRAME = 0x40000,
        WS_SYSMENU = 0x80000,
        WS_TABSTOP = 0x10000,
        WS_VISIBLE = 0x10000000,
        WS_VSCROLL = 0x200000
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    public delegate IntPtr WNDPROC(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential)]
    public struct WNDCLASS
    {
        public uint style;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public WNDPROC lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpszMenuName;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpszClassName;
    }
    public class User32
    {
        private const string user32 = "user32.dll";

        public const uint IDC_ARROW = 32512;

        public const uint IDI_APPLICATION = 32512;
        public const uint IDI_WINLOGO = 32517;

        public const int SW_HIDE = 0;

        public const uint CS_VREDRAW = 0x1;
        public const uint CS_HREDRAW = 0x2;
        public const uint CS_OWNDC = 0x20;

        public const uint WS_EX_CLIENTEDGE = 0x00000200;

        [DllImport(user32, CallingConvention = CallingConvention.Winapi, SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern ushort RegisterClass(ref WNDCLASS lpWndClass);

        [DllImport(user32, CallingConvention = CallingConvention.Winapi, SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern ushort UnregisterClass([MarshalAs(UnmanagedType.LPTStr)] string lpClassName, IntPtr hInstance);

        [DllImport(user32, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport(user32, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        public static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);

        [DllImport(user32, CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport(user32, CallingConvention = CallingConvention.Winapi, SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        public static extern IntPtr CreateWindowEx(uint dwExStyle, [MarshalAs(UnmanagedType.LPTStr)] string lpClassName, [MarshalAs(UnmanagedType.LPTStr)] string lpWindowName, WindowStyles dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

        public static IntPtr CreateWindow(string lpClassName, string lpWindowName, WindowStyles dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam)
        {
            return CreateWindowEx(0, lpClassName, lpWindowName, dwStyle, x, y, nWidth, nHeight, hWndParent, hMenu, hInstance, lpParam);
        }

        [DllImport(user32, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport(user32, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport(user32, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport(user32, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindow(IntPtr hWnd);

        [DllImport(user32, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport(user32, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AdjustWindowRectEx(ref RECT lpRect, WindowStyles dwStyle, bool bMenu, uint dwExStyle);

        [DllImport(user32, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);
    }

    public class Win32Window : IDisposable
    {
        private ushort classRegistration;

        public string WindowClassName { get; }

        public IntPtr WindowHandle { get; private set; }

        public IntPtr DeviceContextHandle { get; private set; }

        public Win32Window(string className)
        {
            WindowClassName = className;

            var wc = new WNDCLASS
            {
                cbClsExtra = 0,
                cbWndExtra = 0,
                hbrBackground = IntPtr.Zero,
                hCursor = User32.LoadCursor(IntPtr.Zero, (int)User32.IDC_ARROW),
                hIcon = User32.LoadIcon(IntPtr.Zero, (IntPtr)User32.IDI_APPLICATION),
                hInstance = Kernel32.CurrentModuleHandle,
                lpfnWndProc = (WNDPROC)User32.DefWindowProc,
                lpszClassName = WindowClassName,
                lpszMenuName = null,
                style = User32.CS_HREDRAW | User32.CS_VREDRAW | User32.CS_OWNDC
            };

            classRegistration = User32.RegisterClass(ref wc);
            if (classRegistration == 0)
                throw new Exception($"Could not register window class: {className}");

            WindowHandle = User32.CreateWindow(
                WindowClassName,
                $"The Invisible Man ({className})",
                WindowStyles.WS_OVERLAPPEDWINDOW,
                0, 0,
                1, 1,
                IntPtr.Zero, IntPtr.Zero, Kernel32.CurrentModuleHandle, IntPtr.Zero);
            if (WindowHandle == IntPtr.Zero)
                throw new Exception($"Could not create window: {className}");

            DeviceContextHandle = User32.GetDC(WindowHandle);
            if (DeviceContextHandle == IntPtr.Zero)
            {
                Dispose();
                throw new Exception($"Could not get device context: {className}");
            }
        }

        public void Dispose()
        {
            if (WindowHandle != IntPtr.Zero)
            {
                if (DeviceContextHandle != IntPtr.Zero)
                {
                    User32.ReleaseDC(WindowHandle, DeviceContextHandle);
                    DeviceContextHandle = IntPtr.Zero;
                }

                User32.DestroyWindow(WindowHandle);
                WindowHandle = IntPtr.Zero;
            }

            if (classRegistration != 0)
            {
                User32.UnregisterClass(WindowClassName, Kernel32.CurrentModuleHandle);
                classRegistration = 0;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PIXELFORMATDESCRIPTOR
    {
        public ushort nSize;
        public ushort nVersion;
        public uint dwFlags;
        public byte iPixelType;
        public byte cColorBits;
        public byte cRedBits;
        public byte cRedShift;
        public byte cGreenBits;
        public byte cGreenShift;
        public byte cBlueBits;
        public byte cBlueShift;
        public byte cAlphaBits;
        public byte cAlphaShift;
        public byte cAccumBits;
        public byte cAccumRedBits;
        public byte cAccumGreenBits;
        public byte cAccumBlueBits;
        public byte cAccumAlphaBits;
        public byte cDepthBits;
        public byte cStencilBits;
        public byte cAuxBuffers;
        public byte iLayerType;
        public byte bReserved;
        public int dwLayerMask;
        public int dwVisibleMask;
        public int dwDamageMask;
    }

    internal class Gdi32
    {
        private const string gdi32 = "gdi32.dll";

        public const byte PFD_TYPE_RGBA = 0;

        public const byte PFD_MAIN_PLANE = 0;

        public const uint PFD_DRAW_TO_WINDOW = 0x00000004;
        public const uint PFD_SUPPORT_OPENGL = 0x00000020;

        [DllImport(gdi32, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetPixelFormat(IntPtr hdc, int iPixelFormat, [In] ref PIXELFORMATDESCRIPTOR ppfd);

        [DllImport(gdi32, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        public static extern int ChoosePixelFormat(IntPtr hdc, [In] ref PIXELFORMATDESCRIPTOR ppfd);

        [DllImport(gdi32, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SwapBuffers(IntPtr hdc);
    }
}
