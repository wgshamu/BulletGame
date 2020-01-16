using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    public class Thing
    {
        public Point pt;
        public virtual void Move()
        {

        }
    }
    public class Enemy : Thing
    {
        
        SolidColorBrush color = new SolidColorBrush();
        double diameter;
        Canvas canvas = null;
        Ellipse circle;
        
        int pattern;
        Vector dir;
        double name;
        bool isdead;
        public Enemy(Canvas canvas, Point pt, int pattern, SolidColorBrush color, double diameter, Vector dir, double name)

        {
            this.diameter = diameter;
            this.color = color;
            this.canvas = canvas;
            this.pt = pt;
            this.pattern = pattern;
            this.dir = dir;
            this.name = name;
            circle = new Ellipse() { Fill = color, Width = diameter, Height = diameter };
            canvas.Children.Add(circle);
            Canvas.SetLeft(circle, pt.X );
            Canvas.SetTop(circle, pt.Y);
        }
        public void die(Canvas canvas)
        {
            canvas.Children.Remove(circle);
            MainWindow.diethings.Add(this);
        }
        public void Movebounce()
        {
            pt += dir;
            if (pt.X > canvas.Width || pt.X < 0)
            {
                dir.X = -dir.X;
            }

            if (pt.Y > canvas.Height || pt.Y < 0)
            {
                dir.Y = -dir.Y;
            }

            Canvas.SetLeft(circle, pt.X);
            Canvas.SetTop(circle, pt.Y);
        }
        public void MoveHome()
        {
            Vector home = (pt - MainWindow.player.pt);
            if (home.X > 0)
            {
                pt.X -= 10;
            }
            else
                pt.X += 10;
            if (home.Y > 0)
            {
                pt.Y -= 10;
            }
            else
                pt.Y += 10;
            Canvas.SetLeft(circle, pt.X);
            Canvas.SetTop(circle, pt.Y);
            if (pt.X > 1000 || pt.X < 0 || pt.Y > 1000 || pt.Y < 0)
            {
                isdead = true;
            }
        }
        public void Movemultiple()
        {
            pt += (dir * name);
            
            Canvas.SetLeft(circle, pt.X);
            Canvas.SetTop(circle, pt.Y);
            if (pt.X > 1000 || pt.X < 0 ||pt.Y > 1000 || pt.Y <0)
            {
                isdead = true;
            }
        }
        public void Movespin()
        {

        }
        public void MoveLine()
        {
           Vector Dire =  new Vector(name * Math.Cos(MainWindow.movehash/6 * Math.PI / 180), name * Math.Sin(MainWindow.movehash/6 * Math.PI / 180));
            
          
            Canvas.SetLeft(circle, Dire.X + 500);
            Canvas.SetTop(circle, Dire.Y + 500);

        }
        public override void Move()
        {
            
            if (pattern == 1)
            {
                Movebounce();
            }
            if (pattern == 2)
            {
                Movemultiple();
            }
            if (pattern == 3)
            {
                MoveLine();
            }
            if (pattern == 4)
            {
                MoveHome();
            }
            if (isdead)
            {
                die(canvas); 
            }
        }
        
    }

    public class Bomb : Thing
    {

        SolidColorBrush color = new SolidColorBrush();
        double diameter;
        Canvas canvas = null;
        Ellipse circle;

        int pattern;
        Vector dir;
        double name;
        bool isdead;
        public Bomb(Canvas canvas, Point pt, int pattern, SolidColorBrush color, double diameter, Vector dir, double name)

        {
            this.diameter = diameter;
            this.color = color;
            this.canvas = canvas;
            this.pt = pt;
            this.pattern = pattern;
            this.dir = dir;
            this.name = name;
            circle = new Ellipse() { Fill = color, Width = diameter, Height = diameter };
            canvas.Children.Add(circle);
            Canvas.SetLeft(circle, pt.X);
            Canvas.SetTop(circle, pt.Y);
        }
        public void die(Canvas canvas)
        {
            canvas.Children.Remove(circle);
            MainWindow.diethings.Add(this);
        }
        public void Movebounce()
        {
            pt += dir;
            if (pt.X >= canvas.Width || pt.X < 0)
            {
                dir.X = -dir.X;
            }

            if (pt.Y >= canvas.Height || pt.Y < 0)
            {
                dir.Y = -dir.Y;
            }

            Canvas.SetLeft(circle, pt.X);
            Canvas.SetTop(circle, pt.Y);
        }
        public void MoveHome()
        {
            Vector home = (pt - MainWindow.player.pt);
            if (home.X > 0)
            {
                pt.X -= 10;
            }
            else
                pt.X += 10;
            if (home.Y > 0)
            {
                pt.Y -= 10;
            }
            else
                pt.Y += 10;
            Canvas.SetLeft(circle, pt.X);
            Canvas.SetTop(circle, pt.Y);
            if (pt.X > 1000 || pt.X < 0 || pt.Y > 1000 || pt.Y < 0)
            {
                isdead = true;
            }
        }
        public void Movemultiple()
        {
            pt += (dir * name);

            Canvas.SetLeft(circle, pt.X);
            Canvas.SetTop(circle, pt.Y);
            if (pt.X > 1000 || pt.X < 0 || pt.Y > 1000 || pt.Y < 0)
            {
                isdead = true;
            }
        }
        public void Movespin()
        {

        }
        public void MoveLine()
        {
            Vector Dire = new Vector(name * Math.Cos(MainWindow.movehash / 6 * Math.PI / 180), name * Math.Sin(MainWindow.movehash / 6 * Math.PI / 180));


            Canvas.SetLeft(circle, Dire.X + 500);
            Canvas.SetTop(circle, Dire.Y + 500);

        }
        public override void Move()
        {

            if (pattern == 1)
            {
                Movebounce();
            }
            if (pattern == 2)
            {
                Movemultiple();
            }
            if (pattern == 3)
            {
                MoveLine();
            }
            if (pattern == 4)
            {
                MoveHome();
            }
            if (isdead)
            {
                die(canvas);
            }
        }

    }
    public class Player : Thing
    {
        //614 x 564
        //<Image Width = "200" Source = "Images\red_rock_01.jpg"  
        //            VerticalAlignment = "Top" Margin = "30"/>         static double width;
        Canvas canvas = null;
        
        Vector dir;
        Image image;
        Ellipse circle;
        double width = 100;
        int step = 0;  // 0..15
        int speed = 10; //the speed change this as needed
        BitmapImage[] Dead = new BitmapImage[16];
        BitmapImage[] Idle = new BitmapImage[16];
        BitmapImage[] Jump = new BitmapImage[16];
        BitmapImage[] Walk = new BitmapImage[16];
        BitmapImage[] Run = new BitmapImage[16];
        BitmapImage[] mode;
        public Player(Canvas canvas, Point pt, Vector dir)
        {
            this.canvas = canvas;
            this.pt = pt;
            this.dir = dir;
            for (int i = 0; i < 16; i++)
            {
                Dead[i] = new BitmapImage(new Uri($"Sprites\\Dead ({i}).png", UriKind.Relative));
                Idle[i] = new BitmapImage(new Uri($"Sprites\\Idle ({i}).png", UriKind.Relative));
                Jump[i] = new BitmapImage(new Uri($"Sprites\\Jump ({i}).png", UriKind.Relative));
                Walk[i] = new BitmapImage(new Uri($"Sprites\\Walk ({i}).png", UriKind.Relative));
                Run[i] = new BitmapImage(new Uri($"C:\\Users\\Shamu\\source\\repos\\WpfApp1\\WpfApp1\\Sprites\\Sprites\\Run ({i}).png", UriKind.Relative));
            }
            mode = Run;
            circle = new Ellipse() { Fill = new SolidColorBrush(Colors.Red), Width = 50, Height = 50 };
            MainWindow.things.Add(this);
            image = new Image() { Width = width, Height = width * 564 / 614, Source = mode[step] };
            canvas.Children.Add(circle);
            Canvas.SetLeft(circle, pt.X);
            Canvas.SetTop(circle, pt.Y);
            canvas.Children.Add(image);
            Canvas.SetLeft(image, pt.X);
            Canvas.SetTop(image, pt.Y);

        }

        public override void Move()
        {

           // step = (step + 1) % 16;
           // image.Source = mode[step];
            if (MainWindow.iskeydown(Key.Left))
            {
                pt.X -= speed;
                if (MainWindow.iskeydown(Key.Down))
                {
                    pt.X += speed - (speed / Math.Sqrt(2));
                 pt.Y += (speed / Math.Sqrt(2));     
                        }
                else if (MainWindow.iskeydown(Key.Up))
                {
                    pt.X += speed - (speed / Math.Sqrt(2));
                    pt.Y -= (speed / Math.Sqrt(2));
                }
            }
            else if (MainWindow.iskeydown(Key.Right))
            {
                pt.X += speed;
                if (MainWindow.iskeydown(Key.Down))
                {
                    pt.X -= speed - (speed / Math.Sqrt(2));
                    pt.Y += (speed / Math.Sqrt(2));
                }
                else if (MainWindow.iskeydown(Key.Up))
                {
                    pt.X -= speed - (speed / Math.Sqrt(2));
                    pt.Y -= (speed / Math.Sqrt(2));
                }
            }
            else if (MainWindow.iskeydown(Key.Down))
            {
                pt.Y += speed;
            }

          
            
            else if (MainWindow.iskeydown(Key.Up))
            {
                pt.Y -= speed;
            }
           
            if (pt.X > canvas.Width || pt.X < 0)
            {
                dir.X = -dir.X;
                mode = Dead;
            }

            if (pt.Y > canvas.Height || pt.Y < 0)
            {
                dir.Y = -dir.Y;
            }
            Canvas.SetLeft(circle, pt.X);
            Canvas.SetTop(circle, pt.Y);
            Canvas.SetLeft(image, pt.X);
            Canvas.SetTop(image, pt.Y);
        }

    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Thing> diethings = new List<Thing>();
       public static Player player;
        public static int movehash = 0;
        bool movetrue = false;
       public static List<Thing> things = new List<Thing>();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
      static  Dictionary<Key, bool> keydictionary = new Dictionary<Key, bool>(); 
        
        public MainWindow()
        {
            InitializeComponent();
            
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            this.KeyDown += new KeyEventHandler(buttonkeydown);
            this.KeyUp += new KeyEventHandler(buttonkeydown);
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1.0 / 60);
            dispatcherTimer.Start();
             player = new Player(canvas, new Point(100, 100), new Vector(1, 0));
       //     for (int i = 0; i < 360; i += 36)
       //     {
       //
       //         things.Add(new Bomb(canvas, new Point(500, 500), 1, new SolidColorBrush(Colors.Black), 10.0, new Vector(10 * Math.Cos(i * Math.PI / 180), 10 * Math.Sin(i * Math.PI / 180)), 1));
       //     }
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (movetrue)
                movehash+=6;
            
            foreach (var thing in things)
            {
                thing.Move();

            }
            foreach (var thing in diethings)
            {
                things.Remove(thing);

            }
           
            diethings.Clear();
        }
        public static bool iskeydown(Key key)
        {
            if(keydictionary.ContainsKey(key))
            {
                return keydictionary[key];

            }
            return false;
        }
        private void buttonkeydown(object sender, KeyEventArgs e)

        {
            keydictionary[e.Key] = e.IsDown;

            
                if (e.Key == Key.A)
            {

            
            //makes a circle of dots at an interval of 1/10
            /*  for (int i = 0; i < 360; i += 36)
               {
                   things.Add(new Bomb(canvas, new Point(500, 500), 1, new SolidColorBrush(Colors.Black), 10.0, new Vector(10 * Math.Cos(i * Math.PI / 180), 10 * Math.Sin(i * Math.PI / 180)), 1));
               }
              */ movetrue = true;
               // makes a circle of dots that desync slightly from an interval of 1/10
               for (int i = 0; i < 360; i += 36)
               {
                   int x = i + movehash;
                   things.Add(new Bomb(canvas, new Point(500, 500), 1, new SolidColorBrush(Colors.Red), 10.0, new Vector(10 * Math.Cos(x * Math.PI / 180), 10 * Math.Sin(x * Math.PI / 180)), 1));
               } 

             //makes a line of dots
           
            movetrue = true;
         /*         for (int i = 0; i <= 600; i += 30)
                  {
                      things.Add(new Bomb(canvas, new Point(i, 475), 3, new SolidColorBrush(Colors.Blue), 30.0, new Vector(10 * Math.Cos(movehash * Math.PI / 180), 30 * Math.Sin(movehash * Math.PI / 180)), i));
                  }
                  //makes multiple dots at varying speeds and shoots them out.
              /*       for (double x = 1.0; x<2.0; x+= .1)
                   {

                           things.Add(new Bomb(canvas, new Point(500, 500), 2, new SolidColorBrush(Colors.Green), 30.0, new Vector(10 * Math.Cos(movehash * 6 * Math.PI / 180), 10 * Math.Sin(movehash * 6 * Math.PI / 180) ), x));

                   }
              /*  for (int i = 0; i < 360; i += 36)
                {
                    things.Add(new Bomb(canvas, new Point(500, 500), 4, new SolidColorBrush(Colors.Black), 10.0, new Vector(10 * Math.Cos(i * Math.PI / 180), 10 * Math.Sin(i * Math.PI / 180)), 1));
                } */ 
            }
        }
    }

}

