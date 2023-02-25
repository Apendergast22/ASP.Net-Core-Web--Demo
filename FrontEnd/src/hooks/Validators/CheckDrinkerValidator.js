import * as yup from 'yup'

// Validation: value can't cantains incorrect symbols.
const checkNoBadSymbols = value => {
    return !(/[!@#$%^&*()-_+=/\\?><.,`~{}]/.test(value));
}

const today = new Date();
const checkDrinkerShema = yup.object().shape({
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
        birth_day: yup.date().max(today)
    }
)

export default checkDrinkerShema