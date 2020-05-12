import { signout } from '../services/auth-service'

export const userSettings = [
    {
        label: 'Account',
        icon: 'fas fa-user ',
        action: '/account'
    },
    {
        label: 'LogOut',
        icon: 'fas fa-sign-out-alt',
        action: '/signout'
    }
]