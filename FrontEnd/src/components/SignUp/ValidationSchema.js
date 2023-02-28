import * as yup from 'yup'

// Validation: value can't cantains incorrect symbols.
const checkNoBadSymbols = value => {
    return !(/[!@#$%^&*()-_+=/\\?><.,`~{}]/.test(value));
}

const today = new Date();
const signUpShema = yup.object().shape({
    email: yup.string()
        .matches(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/, 'Почта дожна иметь формат \'example@service.net\'')
        .required('Ввод почты обязателен!'),
    password: yup.string()
        .min(6, 'Пароль не может содержать менее 6 символов')
        .max(20, 'Пароль не может содержать более 20 символов')
        .required(),
    name: yup.string().test(
            'symbols-check',
            'Имя не может содержать специальных символов',
            checkNoBadSymbols
        ).required('Введите ваше имя!'),
    surname: yup.string().test(
            'symbols-check',
            'Введите вашу фамилию!',
            checkNoBadSymbols
        ).required('Фамилия не может быть пустой!'),
    patronymic: yup.string().test(
            'symbols-check',
            'Отчество не может содержать специальных символов',
            checkNoBadSymbols
        ),
    birth_day: yup.date().max(today), // https://www.codedaily.io/tutorials/Yup-Date-Validation-with-Custom-Transform
    city: yup.string().test(
            'symbols-check',
            'Город не может содержать специальных символов',
            checkNoBadSymbols
        ).required('Введите ваш город!'),
    street: yup.string().test(
            'symbols-check',
            'Улица не может содержать специальных символов',
            checkNoBadSymbols
        ).required('Введите вашу улицу!')
    }
)

export default signUpShema