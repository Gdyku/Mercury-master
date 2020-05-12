export const elements = [
    {
        name: 'Home',
        icon: 'fas fa-home',
        href: '/'
    },
    {
        name: 'Travels',
        icon: 'fas fa-bus-alt',
        children: [
            {
                name: 'All',
                href: '/new'
            },
            {
                name: 'Best',
                href: '/sch'
            }
        ]
    },
    {
        name: 'About',
        icon: 'fas fa-copy',
        children: [
            {
                name: 'All',
                href: '/account'
            },
            {
                name: 'Best',
                href: ''
            }
        ]
    }
]