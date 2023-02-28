import * as yup from 'yup'
const signInShema = yup.object().shape({
        email: yup.string()
            .matches(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/, 'Почта дожна иметь формат \'example@service.net\'')
            .required('Ввод почты обязателен!'),
        password: yup.string()
            .min(6, 'Пароль не может содержать менее 6 символов')
            .max(20, 'Пароль не может содержать более 20 символов')
            .required('Ввод пароля обязателен!'),
    }
)

export default signInShema