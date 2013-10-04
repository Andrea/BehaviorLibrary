//
//  TestCases.cs
//
//  Author:
//       Thomas H. Jonell <@Net_Gnome>
//
//  Copyright (c) 2013 Thomas H. Jonell
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using CSTester;
using CSLogging;
using BehaviorLibrary;
using BehaviorLibrary.Components;
using BehaviorLibrary.Components.Actions;

namespace Tests
{
	public class TestCases
	{
		public TestCases (){}

		private CSLogger log = CSLogger.Instance;

		[BuildUp]
		public void buildup(){
			log.setEnableLogging (true);
			log.setEnableDebug (true);
			log.setEnableError (true);
			log.setEnableMessage (true);
			log.loadLog("","behaviorLibrary.log");
			log.enterScope ("TestCases");
			log.logMessage ("----------------- STARTING BEHAVIOR LIBRARY TESTS -----------------");
		}

		[TearDown]
		public void teardown(){
			log.enterScope("teardown");
			log.exitScope ();
			log.logMessage ("----------------- ENDING BEHAVIOR LIBRARY TESTS -----------------");
			log.exitScope ();
			log.closeLog ();
		}

		[Test]
		public void testStatefulSeq(){
			log.enterScope("testStatefulSeq");

			bool first = true;

			var foo = new StatefulSequence (new BehaviorAction(delegate(){
				return BehaviorReturnCode.Success;
			}),new BehaviorAction( delegate(){
				if(first){
					first = false;
					return BehaviorReturnCode.Running;
				}else{
					return BehaviorReturnCode.Success;
				}
			}),new BehaviorAction(delegate(){
				return BehaviorReturnCode.Success;
			}));

			new VerificationPoint ().VerifyEquals ("1st running", true, foo.Behave (), BehaviorReturnCode.Running);
			new VerificationPoint ().VerifyEquals ("2nd success", true, foo.Behave (), BehaviorReturnCode.Success);
			new VerificationPoint ().VerifyEquals ("3rd success", true, foo.Behave (), BehaviorReturnCode.Success);

			log.logMessage ("restting first");
			first = true;

			new VerificationPoint ().VerifyEquals ("after reset running", true, foo.Behave (), BehaviorReturnCode.Running);
			new VerificationPoint ().VerifyEquals ("final success", true, foo.Behave (), BehaviorReturnCode.Success);

			log.exitScope ();
		}

		[Test]
		public void testStatefulSel(){
			log.enterScope("testStatefulSel");

			bool first = true;

			var foo = new StatefulSelector (new BehaviorAction (delegate(){
				return BehaviorReturnCode.Failure;
			}), new BehaviorAction (delegate() {
				if(first){
					first = false;
					return BehaviorReturnCode.Running;
				}else{
					return BehaviorReturnCode.Failure;
				}
			}), new BehaviorAction (delegate(){
				return BehaviorReturnCode.Success;
			}));

			new VerificationPoint ().VerifyEquals ("1st running", true, foo.Behave (), BehaviorReturnCode.Running);
			new VerificationPoint ().VerifyEquals ("2nd success", true, foo.Behave (), BehaviorReturnCode.Success);
			new VerificationPoint ().VerifyEquals ("3rd success", true, foo.Behave (), BehaviorReturnCode.Success);

			log.logMessage ("restting first");
			first = true;

			new VerificationPoint ().VerifyEquals ("after reset running", true, foo.Behave (), BehaviorReturnCode.Running);
			new VerificationPoint ().VerifyEquals ("final success", true, foo.Behave (), BehaviorReturnCode.Success);

			log.exitScope ();
		}
	}
}

