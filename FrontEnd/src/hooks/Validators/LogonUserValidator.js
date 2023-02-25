import * as yup from 'yup'
const logonShema = yup.object().shape({
        login: yup.string().matches(/^[aA-zZ]+$/, 'В качестве логина можно использовать только латинские символы!').required(),
        password: yup.string().min(6, 'Пароль не может содержать менее 6 символов').max(20, 'Пароль не может содержать более 20 символов').required(),
    }
)

export default logonShema