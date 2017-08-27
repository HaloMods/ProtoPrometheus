using System;
using System.Collections;
using System.Drawing;

namespace UIControls.HackScriptEditor
{
	public class HighlightDescriptor
	{
		public HighlightDescriptor(string token, Color color, Font font, DescriptorType descriptorType, DescriptorRecognition dr, bool useForAutoComplete)
		{
			if (descriptorType == UIControls.HackScriptEditor.DescriptorType.ToCloseToken)
				throw new ArgumentException("You may not choose ToCloseToken DescriptorType without specifing an end token.");
			Color = color;
			Font = font;
			Token = token;
			DescriptorType = descriptorType;
			DescriptorRecognition = dr;
			CloseToken = null;
			UseForAutoComplete = useForAutoComplete;
		}
		public HighlightDescriptor(string token, string closeToken, Color color, Font font, DescriptorType descriptorType, DescriptorRecognition dr, bool useForAutoComplete)
		{
			Color = color;
			Font = font;
			Token = token;
			DescriptorType = descriptorType;
			CloseToken = closeToken;
			DescriptorRecognition = dr;
			UseForAutoComplete = useForAutoComplete;
		}
		public readonly Color Color;
		public readonly Font Font;
		public readonly string Token;
		public readonly string CloseToken;
		public readonly DescriptorType DescriptorType;
		public readonly DescriptorRecognition DescriptorRecognition; 
		public readonly bool UseForAutoComplete;
	};

	public enum DescriptorType
	{
		/// <summary>
		/// Causes the highlighting of a single word
		/// </summary>
		Word,
		/// <summary>
		/// Causes the entire line from this point on the be highlighted, regardless of other tokens
		/// </summary>
		ToEOL,
		/// <summary>
		/// Highlights all text until the end token;
		/// </summary>
		ToCloseToken
	};

	public enum DescriptorRecognition
	{
		/// <summary>
		/// Only if the whole token is equal to the word
		/// </summary>
		WholeWord,
		/// <summary>
		/// If the word starts with the token
		/// </summary>
		StartsWith,
		/// <summary>
		/// If the word contains the Token
		/// </summary>
		Contains
	};

	public class HighLightDescriptorCollection
	{
		private ArrayList mInnerList = new ArrayList();
		internal HighLightDescriptorCollection()
		{
		}

		public void AddRange(ICollection c)
		{
			mInnerList.AddRange(c);
		}


		#region IList Members
		public bool IsReadOnly
		{
			get	{ return mInnerList.IsReadOnly; }
		}

		public HighlightDescriptor this[int index]
		{
			get	{ return (HighlightDescriptor)mInnerList[index]; }
			set	{ mInnerList[index] = value; }
		}

		public void RemoveAt(int index)
		{
			mInnerList.RemoveAt(index);
		}

		public void Insert(int index, HighlightDescriptor value)
		{
			mInnerList.Insert(index, value);
		}

		public void Remove(HighlightDescriptor value)
		{
			mInnerList.Remove(value);
		}

		public bool Contains(HighlightDescriptor value)
		{
			return mInnerList.Contains(value);
		}

		public void Clear()
		{
			mInnerList.Clear();
		}

		public int IndexOf(HighlightDescriptor value)
		{
			return mInnerList.IndexOf(value);
		}

		public int Add(HighlightDescriptor value)
		{
			return mInnerList.Add(value);
		}

		public bool IsFixedSize
		{
			get	{ return mInnerList.IsFixedSize; }
		}
		#endregion

		#region ICollection Members
		public bool IsSynchronized
		{
			get	{ return mInnerList.IsSynchronized; }
		}

		public int Count
		{
			get	{ return mInnerList.Count; }
		}

		public void CopyTo(Array array, int index)
		{
			mInnerList.CopyTo(array, index);
		}

		public object SyncRoot
		{
			get	{ return mInnerList.SyncRoot; }
		}
		#endregion

		#region IEnumerable Members
		public IEnumerator GetEnumerator()
		{
			return mInnerList.GetEnumerator();
		}
		#endregion
	};

	public class SeperaratorCollection
	{
		private ArrayList mInnerList = new ArrayList();
		internal SeperaratorCollection()
		{
		}

		public void AddRange(ICollection c)
		{
			mInnerList.AddRange(c);
		}

		internal char[] GetAsCharArray()
		{
			return (char[])mInnerList.ToArray(typeof(char));
		}

		#region IList Members
		public bool IsReadOnly
		{
			get	{ return mInnerList.IsReadOnly; }
		}

		public char this[int index]
		{
			get	{ return (char)mInnerList[index]; }
			set	{ mInnerList[index] = value; }
		}

		public void RemoveAt(int index)
		{
			mInnerList.RemoveAt(index);
		}

		public void Insert(int index, char value)
		{
			mInnerList.Insert(index, value);
		}

		public void Remove(char value)
		{
			mInnerList.Remove(value);
		}

		public bool Contains(char value)
		{
			return mInnerList.Contains(value);
		}

		public void Clear()
		{
			mInnerList.Clear();
		}

		public int IndexOf(char value)
		{
			return mInnerList.IndexOf(value);
		}

		public int Add(char value)
		{
			return mInnerList.Add(value);
		}

		public bool IsFixedSize
		{
			get	{ return mInnerList.IsFixedSize; }
		}
		#endregion

		#region ICollection Members
		public bool IsSynchronized
		{
			get	{ return mInnerList.IsSynchronized; }
		}

		public int Count
		{
			get	{ return mInnerList.Count; }
		}

		public void CopyTo(Array array, int index)
		{
			mInnerList.CopyTo(array, index);
		}

		public object SyncRoot
		{
			get	{ return mInnerList.SyncRoot; }
		}
		#endregion

		#region IEnumerable Members
		public IEnumerator GetEnumerator()
		{
			return mInnerList.GetEnumerator();
		}
		#endregion
	};
}