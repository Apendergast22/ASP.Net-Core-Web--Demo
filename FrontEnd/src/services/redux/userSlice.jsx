import { createSlice } from '@reduxjs/toolkit'

// Initial our user state.
const initialUserState = {
    email: "",
    name: "",
    surname: "",
    patronymic: "",
    birthDay: new Date(),
    city: "",
    address: "",
    logon: false
}

export const userSlice = createSlice({
    name: 'user',
    initialState: initialUserState, 
    reducers: {
        logon: (state) => {
            state.logon = true;
            state.email = state.email;
            state.name = state.name;
            state.surname = state.surname;
            state.patronymic = state.patronymic;
            state.birthDay = state.birthDay;
            state.city = state.city;
            state.address = state.address;
        },

        logout: () => initialUserState
    }
})

export const { logon, logout } = userSlice.actions;
export default userSlice.reducer
