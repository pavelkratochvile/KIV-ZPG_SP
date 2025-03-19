using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using ConsoleApp1.Cameras;

namespace ConsoleApp1
{
    public class MyGameWindow : GameWindow
    {

        private List<Block> Walls = new List<Block>();
        private float speed = 1.4f;
        private double[] movingVector = new double[] { 0, 0 };
        private ViewPort Viewport { get; set; }
        private double deltaTime = 0.0;
        private Camera Camera { get; set; }
        private Map Map = new Map();
        private Block Block;

        private Stopwatch stopwatch = new Stopwatch();
        private int frameCount = 0;
        private double lastTime = 0;
        private double fps = 0;



        public MyGameWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            Viewport = new ViewPort()
            {
                Top = 0,
                Left = 0,
                Width = 1,
                Height = 1,
                Control = this
            }
            ;
            Camera = new Camera(Viewport);


            this.CursorState = CursorState.Grabbed;

        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {

            CountFPS();

            float deltaTime = (float)args.Time;
            
            this.MakeCurrent();

            //Camera.SetProjection();
            //Camera.SetView();

            GetMovingVector();

            MakeMove(deltaTime);
            IsExiting();

            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            DrawMap();

            GL.PointSize(10);
            this.SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            Viewport.Set();
        }



        protected override void OnLoad()
        {
            base.OnLoad();
            Shader shader = new Shader("Shaders/Basic.vert", "Shaders/Basic.frag");

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.DepthMask(true);
            GL.ClearDepth(1.0f);
            
            stopwatch.Start();  

            Map.map = Map.MapTranformation("map1.txt");
            GenerateMap(shader);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);

            float sensitivity = 0.002f;

            float deltaX = e.Delta.X * sensitivity;
            float deltaY = e.Delta.Y * sensitivity;

            Camera.RotateY(deltaX);
            Camera.RotateX(deltaY);


        }

        private void IsExiting()
        {
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }
        
        public void CountFPS()
        {
            frameCount++;
            double currentTime = stopwatch.Elapsed.TotalSeconds;

            if(currentTime - lastTime >= 1)
            {
                fps = frameCount / (currentTime - lastTime);
                frameCount = 0;
                lastTime = currentTime;
                this.Title = $"FPS: {fps:F0}";
            }
        }

        private void MakeMove(float deltaTime)
        {
            float movement = deltaTime * speed;
            float dx = (float)(movingVector[0]);
            float dy = (float)(movingVector[1]);

            if (KeyboardState.IsKeyDown(Keys.A)) {Camera.Move(movement * dx, 0, Walls);  }
            if (KeyboardState.IsKeyDown(Keys.D)) {Camera.Move(movement * dx, 0, Walls); }
            if (KeyboardState.IsKeyDown(Keys.W)) {Camera.Move(0, movement * dy, Walls);  }
            if (KeyboardState.IsKeyDown(Keys.S)) {Camera.Move(0, movement * dy, Walls); }
        }

        
        private void GetMovingVector()
        {
            double x = 0;
            double y = 0;

            if (KeyboardState.IsKeyDown(Keys.A)) { x -= 1; }
            if (KeyboardState.IsKeyDown(Keys.D)) { x += 1; }
            if (KeyboardState.IsKeyDown(Keys.W)) { y += 1; }
            if (KeyboardState.IsKeyDown(Keys.S)) { y -= 1; }

            double length = Math.Sqrt(x * x + y * y);
            if (length != 0)
            {
                x /= length;
                y /= length;
            }
            movingVector[0] = x;
            movingVector[1] = y;
        }

        public void DrawMap()
        {
            Walls.Sort((a, b) => (b.position.Z).CompareTo(a.position.Z));

            foreach (Block block in Walls)
            {
                block.Draw(Camera);
            }
        }
        public void GenerateMap(Shader shader)
        {
            int TILE_SIZE = 2;

            for (int i = 0; i < Map.map.Length; i++)
            {
                for (int j = 0; j < Map.map[0].Length; j++)
                {
                    int posX = (+1) * i * TILE_SIZE;
                    int posZ = (+1) * j * TILE_SIZE;

                    if (Map.map[i][j] == 1 || Map.map[i][j] == 4)
                    {
                        Block block = new Block();
                        block.position = new Vector3(posX, 0, posZ);
                        block.Shader = shader;
                        Walls.Add(block);
                    }
                }
            }
        }


        public static void Main()
        {
            GameWindowSettings gws = new GameWindowSettings
            {
                UpdateFrequency = 0.0,
            };
            NativeWindowSettings nws = new NativeWindowSettings()
            {
                Profile = OpenTK.Windowing.Common.ContextProfile.Compatability,
                Title = "ZPG",
                ClientSize = new OpenTK.Mathematics.Vector2i(800, 500),
                DepthBits = 24,

            };
            var zpg = new MyGameWindow(gws, nws);
            zpg.Run();
        }


    }

}