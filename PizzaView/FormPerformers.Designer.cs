﻿namespace PizzaView
{
    partial class FormPerformers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Добавить = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(502, 209);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 46);
            this.button3.TabIndex = 9;
            this.button3.Text = "Обновить";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.buttonRef_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(502, 157);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 46);
            this.button2.TabIndex = 8;
            this.button2.Text = "Удалить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(502, 105);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 46);
            this.button1.TabIndex = 7;
            this.button1.Text = "Изменить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonUpd_Click);
            // 
            // Добавить
            // 
            this.Добавить.Location = new System.Drawing.Point(502, 53);
            this.Добавить.Name = "Добавить";
            this.Добавить.Size = new System.Drawing.Size(115, 46);
            this.Добавить.TabIndex = 6;
            this.Добавить.Text = "Добавить";
            this.Добавить.UseVisualStyleBackColor = true;
            this.Добавить.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(27, 31);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(437, 371);
            this.dataGridView.TabIndex = 5;
            // 
            // FormPerformers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 458);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Добавить);
            this.Controls.Add(this.dataGridView);
            this.Name = "FormPerformers";
            this.Text = "Сотрудники";
            this.Load += new System.EventHandler(this.FormPerformers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Добавить;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}