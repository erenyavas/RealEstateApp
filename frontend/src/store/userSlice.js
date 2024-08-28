import { createSlice } from '@reduxjs/toolkit';

export const userSlice = createSlice({

    name: 'user',
    initialState: {
        firstName: "",
        lastName: "",
        role: "",
        userId: "",
    },
    reducers: {
        setUser: (state, action) => {
            state.firstName = action.payload.firstName;
            state.lastName = action.payload.lastName;
            state.role = action.payload.role;
            state.userId = action.payload.userId;
        },
    },
});

export const { setUser } = userSlice.actions;


export default userSlice.reducer;
