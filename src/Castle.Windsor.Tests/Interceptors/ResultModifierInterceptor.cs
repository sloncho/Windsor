﻿// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Castle.Windsor.Tests.Interceptors
{
	using Castle.DynamicProxy;

	public class ResultModifierInterceptor : IInterceptor
	{
		private readonly int? returnValue;

		public ResultModifierInterceptor()
		{
		}

		public ResultModifierInterceptor(int returnValue)
		{
			this.returnValue = returnValue;
		}

		public void Intercept(IInvocation invocation)
		{
			if (invocation.Method.Name.Equals("Sum"))
			{
				invocation.Proceed();
				var result = invocation.ReturnValue;
				if (!returnValue.HasValue)
				{
					invocation.ReturnValue = ((int)result) + 1;
					return;
				}
				invocation.ReturnValue = returnValue.Value;
				return;
			}

			invocation.Proceed();
		}
	}
}