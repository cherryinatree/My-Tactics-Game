using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyMap
{
     private string key0 = "Alpha0";
     private string key1 = "1";
     private string key2 = "2";
     private string key3 = "3";
     private string key4 = "4";
     private string key5 = "5";
     private string key6 = "6";
     private string key7 = "7";
     private string key8 = "8";
     private string key9 = "9";
     private string key10 = "Q";
     private string key11 = "W";
     private string key12 = "E";
     private string key13 = "R";
     private string key14 = "T";
     private string key15 = "Y";
     private string key16 = "U";
     private string key17 = "I";
     private string key18 = "O";
     private string key19 = "P";
     private string key20 = "A";
     private string key21 = "S";
     private string key22 = "D";
     private string key23 = "F";
     private string key24 = "G";
     private string key25 = "H";
     private string key26 = "J";
     private string key27 = "K";
     private string key28 = "Z";
     private string key29 = "X";
     private string key30 = "C";
     private string key31 = "V";
     private string key32 = "B";
     private string key33 = "N";
     private string key34 = "Escape";
     private string key35 = "Minus";
     private string key36 = "Equals";
     private string key37 = "LeftAlt";
     private string key38 = "Tab";
     private string key39 = "Comma";
     private string key40 = "Period";
     private string key41 = "Tilde";
     private string key42 = "LeftBracket";
     private string key43 = "RightBracket";
     private string key44 = "Semicolon";
     private string key45 = "Backslash";
     private string key46 = "Mouse0";
     private string key47 = "UpArrow";
     private string key48 = "DownArrow";
     private string key49 = "LeftArrow";
    private string key50 = "RightArrow";
    private string key51 = "Space";
    private string key52 = "L";
    private string key53 = "M";

    private List<string> _keys;

    public KeyMap()
    {
        Keys = new List<string>();
        Keys.Add(key0);
        Keys.Add(key1);
        Keys.Add(key2);
        Keys.Add(key3);
        Keys.Add(key4);
        Keys.Add(key5);
        Keys.Add(key6);
        Keys.Add(key7);
        Keys.Add(key8);
        Keys.Add(key9);
        Keys.Add(key10);
        Keys.Add(key11);
        Keys.Add(key12);
        Keys.Add(key13);
        Keys.Add(key14);
        Keys.Add(key15);
        Keys.Add(key16);
        Keys.Add(key17);
        Keys.Add(key18);
        Keys.Add(key19);
        Keys.Add(key20);
        Keys.Add(key21);
        Keys.Add(key22);
        Keys.Add(key23);
        Keys.Add(key24);
        Keys.Add(key25);
        Keys.Add(key26);
        Keys.Add(key27);
        Keys.Add(key28);
        Keys.Add(key29);
        Keys.Add(key30);
        Keys.Add(key31);
        Keys.Add(key32);
        Keys.Add(key33);
        Keys.Add(key34);
        Keys.Add(key35);
        Keys.Add(key36);
        Keys.Add(key37);
        Keys.Add(key38);
        Keys.Add(key39);
        Keys.Add(key40);
        Keys.Add(key41);
        Keys.Add(key42);
        Keys.Add(key43);
        Keys.Add(key44);
        Keys.Add(key45);
        Keys.Add(key46);
        Keys.Add(key47);
        Keys.Add(key48);
        Keys.Add(key49);
        Keys.Add(key50);
        Keys.Add(key51);
        Keys.Add(key52);
        Keys.Add(key53);
    }

    public KeyMap(MainSaveData data)
    {
        Keys = data.TheKeyMap.Keys;
    }

    public List<string> Keys
    {
        get { return _keys; }
        set { _keys = value; }
    }
}
