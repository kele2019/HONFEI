using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presale.Process.EmployeeTraining.Entity
{
	public class PractiseEntity
	{
		public PractiseEntity() { }
		public PractiseEntity(string Question, string answerA, string answerB, string answerC, string answerD, string answerE, string answerF, string answer) 
		{
			this.Question = Question;
			this.answerA = answerA;
			this.answerB = answerB;
			this.answerC = answerC;
			this.answerD = answerD;
			this.answerE = answerE;
			this.answerF = answerF;
			this.answer = answer;
		}
		public string Question { get; set; }
		public string answerA { get; set; }
		public string answerB { get; set; }
		public string answerC { get; set; }
		public string answerD { get; set; }
		public string answerE { get; set; }
		public string answerF { get; set; }
		public string answer { get; set; }
	}
}