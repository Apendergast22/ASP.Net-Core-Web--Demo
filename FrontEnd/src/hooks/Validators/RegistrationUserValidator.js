import * as yup from 'yup'

// Validation: value can't cantains incorrect symbols.
const checkNoBadSymbols = value => {
    return !(/[!@#$%^&*()-_+=/\\?><.,`~{}]/.test(value));
}

const today = new Date();
const registrationShema = yup.object().shape({
        login: yup.string().matches(/^[aA-zZ]+$/, 'В качестве логина можно использовать только латинские символы!').required(),
        password: yup.string().min(6, 'Пароль не может содержать менее 6 символов').max(20, 'Пароль не может содержать более 20 символов').required(),
        name: yup.string().test(
            'symbols-check',
            'Имя не может содержать специальных символов',
            checkNoBadSymbols
        ).required(),
        surname: yup.string().test(
            'symbols-check',
            'Фамилия не может содержать специальных символов',
            checkNoBadSymbols
        ).required(),
        patronymic: yup.string().test(
            'symbols-check',
            'Отчество не может содержать специальных символов',
            checkNoBadSymbols
        ).required(),
        birth_day: yup.date().max(today), // https://www.codedaily.io/tutorials/Yup-Date-Validation-with-Custom-Transform
        city: yup.string().test(
            'symbols-check',
            'Город не может содержать специальных символов',
            checkNoBadSymbols
        ).required(),
        street: yup.string().test(
            'symbols-check',
            'Улица не может содержать специальных символов',
            checkNoBadSymbols
        ).required()
    }
)

export default registrationShema