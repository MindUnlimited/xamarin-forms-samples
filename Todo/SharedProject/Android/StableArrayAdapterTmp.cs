/*
 * Copyright (C) 2013 The Android Open Source Project
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

//package com.example.android.listviewdragginganimation;

using Android.Content;
//using android.widget.ArrayAdapter;

using Android.Widget;
//using java.util.HashMap;
//using java.util.List;
using Java.Lang;
using System.Collections.Generic;

public class StableArrayAdapter : ArrayAdapter<String> {

    readonly int INVALID_ID = -1;

    Dictionary<String, int> mIdMap = new Dictionary<String, int>();
    //HashMap<String, Integer> mIdMap = new HashMap<String, Integer>();

    public StableArrayAdapter(Context context, int textViewResourceId, List<String> objects) : base(context, textViewResourceId, objects) {
        //base(context, textViewResourceId, objects);
        
        for (int i = 0; i < objects.Count; ++i) {
            mIdMap.Add(objects[i], i);
            //mIdMap.put(objects.get(i), i);
        }
    }

    override public long getItemId(int position) {
        if (position < 0 || position >= mIdMap.Count) {
            return INVALID_ID;
        }
        String item = this.GetItem(position);// getItem(position);
        return mIdMap[item];
        //return mIdMap.get(item);
    }

    override public bool hasStableIds() {
        return true;
    }
}
