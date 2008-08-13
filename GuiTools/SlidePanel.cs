using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DreamBeam
{
	public class SlidePanel : UserControl
	{
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Timer timer1;
		protected Control _oOwner;
		public enum SLIDE_DIRECTION {TOP, LEFT, BOTTOM, RIGHT};
		protected SLIDE_DIRECTION _eSlideDirection;
		private float _fRatio; 
		private float _fStep;
		private bool _bExpand;
		private SizeF _oOffset;
		private SizeF _oStep;
		private Point _oOrigin;
		/// <summary>
		/// Return the state of the form (expanded or not)
		/// </summary>
		public bool IsExpanded
		{
			get
			{
				return _bExpand;
			}
		}
		/// <summary>
		/// Direction of sliding
		/// </summary>
		public SLIDE_DIRECTION SlideDirection
		{
			set
			{
				_eSlideDirection = value;
			}
		}
		/// <summary>
		/// Slide step of the motion
		/// </summary>
		public float SlideStep
		{
			set
			{
				_fStep = value;
			}
		}
		/// <summary>
		/// Default constructor disabled, this requires a panel;
		/// </summary>
		/*public SlidePanel() : this(null, 0)
		{
		}*/
		/// <summary>
		/// Constructor with parent window and step of sliding motion
		/// </summary>
		public SlidePanel(Control poOwner, float pfStep)
		{
			InitializeComponent();
			_oOwner = poOwner;
			_fRatio = 0.0f;
			SlideStep = pfStep;            
            _oOwner.Resize += new System.EventHandler(this.SlidePanel_Resize);
            this.Size = _oOwner.Size;
            this.Init();
		}


        void SlidePanel_Resize(object sender, EventArgs e)
        {
            this.Size = _oOwner.Size;
        }

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // SlidePanel
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Name = "SlidePanel";
            this.Size = new System.Drawing.Size(318, 190);
            this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// Use this method to start the slide motion (in ou out) 
		/// according to the slide direction
		/// </summary>
		public void Slide()
		{                   
            this.Init();
            if (!_bExpand) Show();

			_bExpand = !_bExpand;            
			timer1.Start();
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
           
			if (_bExpand)
			{
				_fRatio += _fStep;
				_oOffset += _oStep;
                if (_fRatio >= 1)
                {
                    timer1.Stop();
                    SetOffset();
                    
                }
                
			}
			else
			{
				_fRatio -= _fStep;
				_oOffset -= _oStep;
                if (_fRatio <= 0)
                {
                    timer1.Stop();
                    _oOffset = new Size(0, 0);
                    this.Hide();
                }
			}
			SetLocation();
            
		}
		private void SetLocation()
		{            
            Location = _oOrigin + _oOffset.ToSize();
		}

		private void SlideDialog_Move(object sender, System.EventArgs e)
		{
			SetSlideLocation();
			SetLocation();
		}

		private void SlideDialog_Closed(object sender, System.EventArgs e)
		{

		}

        private void SetOffset()
        {
            if (_oOwner != null)
            {
                switch (_eSlideDirection)
                {
                    case SLIDE_DIRECTION.BOTTOM:
                        _oOffset = new Size(0, this.Height);
                        break;
                    case SLIDE_DIRECTION.LEFT:
                        _oOffset = new Size(-this.Width, 0);
                        break;
                    case SLIDE_DIRECTION.TOP:
                        _oOffset = new Size(0, -this.Height);
                        break;
                    case SLIDE_DIRECTION.RIGHT:
                        _oOffset = new Size(this.Width, 0);
                        break;
                }
            }
        }

		private void SetSlideLocation()
		{            
			if (_oOwner != null)
			{				
				switch (_eSlideDirection)
				{
					case SLIDE_DIRECTION.BOTTOM:
                        _oOrigin.X = 0;                           
                        _oOrigin.Y = - this.Height;                        
                        _oStep = new SizeF(0, this.Height * _fStep);
						break;
					case SLIDE_DIRECTION.LEFT:
                        _oOrigin.X = Width;
                        _oOrigin.Y = 0;
						_oStep = new SizeF(- Width * _fStep, 0);
						break;
					case SLIDE_DIRECTION.TOP:
                        _oOrigin.X = 0;
                        _oOrigin.Y = Height-1;
						_oStep = new SizeF(0, - Height * _fStep);
						break;
					case SLIDE_DIRECTION.RIGHT:                       
						_oOrigin.X =  - Width;
                        _oOrigin.Y = 0;
						_oStep = new SizeF(Width * _fStep, 0);
						break;
				}
			}
		}
        
		protected void Init()
		{
			SetSlideLocation();
			SetLocation();
		}
	}
}
