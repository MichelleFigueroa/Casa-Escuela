// ============================================
// DARK MODE TOGGLE SYSTEM
// ============================================

// Aplicar tema inmediatamente desde localStorage (antes de que cargue el body)
(function () {
    const THEME_KEY = 'theme-preference';
    const DARK_MODE_CLASS = 'dark-mode';
    const storedTheme = localStorage.getItem(THEME_KEY);

    if (storedTheme === 'dark') {
        // Agregar clase inline antes de que se renderice la página
        document.documentElement.classList.add('dark-mode-loading');
    }
})();

// Esperar a que el DOM esté listo
document.addEventListener('DOMContentLoaded', function () {
    'use strict';

    // Clase para manejar el tema
    const THEME_KEY = 'theme-preference';
    const DARK_MODE_CLASS = 'dark-mode';

    const ThemeManager = {
        // Obtener preferencia guardada
        getStoredTheme: function () {
            return localStorage.getItem(THEME_KEY);
        },

        // Guardar preferencia
        setStoredTheme: function (theme) {
            localStorage.setItem(THEME_KEY, theme);
        },

        // Obtener preferencia del sistema
        getSystemTheme: function () {
            return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
        },

        // Obtener tema actual
        getPreferredTheme: function () {
            const storedTheme = this.getStoredTheme();
            if (storedTheme) {
                return storedTheme;
            }
            return this.getSystemTheme();
        },

        // Verificar si está en dark mode
        isDarkMode: function () {
            return document.body.classList.contains(DARK_MODE_CLASS);
        },

        // Aplicar tema
        applyTheme: function (theme) {
            if (!document.body) {
                console.error('Body no está disponible aún');
                return;
            }

            if (theme === 'dark') {
                document.body.classList.add(DARK_MODE_CLASS);
                document.documentElement.classList.add(DARK_MODE_CLASS);
            } else {
                document.body.classList.remove(DARK_MODE_CLASS);
                document.documentElement.classList.remove(DARK_MODE_CLASS);
            }

            // Limpiar clase temporal
            document.documentElement.classList.remove('dark-mode-loading');

            // Actualizar el estado del botón
            this.updateToggleButton(theme);

            // Disparar evento personalizado
            window.dispatchEvent(new CustomEvent('themeChanged', { detail: { theme } }));

            console.log('🎨 Tema aplicado:', theme);
        },

        // Alternar tema
        toggleTheme: function () {
            const currentTheme = document.body.classList.contains(DARK_MODE_CLASS) ? 'dark' : 'light';
            const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
            this.setStoredTheme(newTheme);
            this.applyTheme(newTheme);
        },

        // Actualizar botón toggle
        updateToggleButton: function (theme) {
            const toggleBtns = document.querySelectorAll('.theme-toggle-btn');
            toggleBtns.forEach(btn => {
                const isDark = theme === 'dark';
                btn.setAttribute('aria-pressed', isDark);
                btn.title = isDark ? 'Cambiar a modo claro' : 'Cambiar a modo oscuro';
            });
        },

        // Inicializar
        init: function () {
            // Aplicar tema inicial
            const theme = this.getPreferredTheme();
            this.applyTheme(theme);

            // Escuchar cambios en la preferencia del sistema
            window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', (e) => {
                if (!this.getStoredTheme()) {
                    this.applyTheme(e.matches ? 'dark' : 'light');
                }
            });
        }
    };

    // ============================================
    // INTERCEPTAR SWEETALERT PARA DARK MODE
    // ============================================

    // Función para aplicar tema oscuro a SweetAlert
    function applySweetAlertDarkTheme() {
        if (!ThemeManager.isDarkMode()) return {};

        return {
            background: '#16213e',
            color: '#e4e4e7',
            confirmButtonColor: '#3b82f6',
            cancelButtonColor: '#374151',
            customClass: {
                popup: 'swal-dark-mode',
                title: 'swal-dark-title',
                htmlContainer: 'swal-dark-text',
                confirmButton: 'swal-dark-confirm',
                cancelButton: 'swal-dark-cancel'
            }
        };
    }

    // Interceptar Swal.fire si existe SweetAlert2
    if (typeof Swal !== 'undefined') {
        const originalFire = Swal.fire;

        Swal.fire = function (...args) {
            // Si se pasa un objeto de configuración
            if (args.length === 1 && typeof args[0] === 'object') {
                const darkTheme = applySweetAlertDarkTheme();
                args[0] = { ...darkTheme, ...args[0] };
            }
            // Si se pasan parámetros individuales (title, text, icon)
            else if (args.length > 0) {
                const darkTheme = applySweetAlertDarkTheme();
                const config = {
                    title: args[0],
                    text: args[1],
                    icon: args[2],
                    ...darkTheme
                };
                return originalFire.call(this, config);
            }

            return originalFire.apply(this, args);
        };

        console.log('✅ SweetAlert2 interceptado para dark mode');
    }

    // Interceptar swal (SweetAlert 1.x) si existe
    if (typeof swal !== 'undefined') {
        const originalSwal = swal;

        window.swal = function (...args) {
            // Aplicar clase dark mode al popup después de crearlo
            const result = originalSwal.apply(this, args);

            if (ThemeManager.isDarkMode()) {
                setTimeout(() => {
                    const swalModal = document.querySelector('.sweet-alert, .swal-modal');
                    if (swalModal) {
                        swalModal.style.backgroundColor = '#16213e';
                        swalModal.style.color = '#e4e4e7';

                        const title = swalModal.querySelector('h2, .swal-title');
                        if (title) title.style.color = '#e4e4e7';

                        const text = swalModal.querySelector('p, .swal-text');
                        if (text) text.style.color = '#e4e4e7';
                    }
                }, 10);
            }

            return result;
        };

        console.log('✅ SweetAlert 1.x interceptado para dark mode');
    }

    // Escuchar cambios de tema para actualizar SweetAlerts abiertos
    window.addEventListener('themeChanged', function (e) {
        const isDark = e.detail.theme === 'dark';

        // Actualizar SweetAlert2 si está abierto
        const swal2Popup = document.querySelector('.swal2-popup');
        if (swal2Popup) {
            if (isDark) {
                swal2Popup.style.backgroundColor = '#16213e';
                swal2Popup.style.color = '#e4e4e7';
            } else {
                swal2Popup.style.backgroundColor = '';
                swal2Popup.style.color = '';
            }
        }

        // Actualizar SweetAlert 1.x si está abierto
        const swalModal = document.querySelector('.sweet-alert, .swal-modal');
        if (swalModal) {
            if (isDark) {
                swalModal.style.backgroundColor = '#16213e';
                swalModal.style.color = '#e4e4e7';
            } else {
                swalModal.style.backgroundColor = '';
                swalModal.style.color = '';
            }
        }
    });

    // Inicializar el sistema de temas
    ThemeManager.init();

    // Crear e insertar el botón toggle en el header
    function createToggleButton() {
        // Buscar el contenedor donde insertar el botón
        const headerList = document.querySelector('.pc-header .ms-auto ul');

        if (!headerList) {
            console.error('No se encontró el contenedor del header');
            return;
        }

        // Verificar si ya existe el botón
        if (document.querySelector('.theme-toggle-btn')) {
            console.log('El botón toggle ya existe');
            return;
        }

        // Crear el elemento li para el botón
        const themeToggleContainer = document.createElement('li');
        themeToggleContainer.className = 'pc-h-item d-flex align-items-center me-3';
        themeToggleContainer.innerHTML = `
            <button class="theme-toggle-btn" 
                    type="button" 
                    aria-label="Cambiar tema"
                    title="Cambiar tema">
                <i class="ti ti-sun theme-toggle-icon sun"></i>
                <i class="ti ti-moon theme-toggle-icon moon"></i>
            </button>
        `;

        // Insertar antes del último elemento (perfil de usuario)
        const userProfile = headerList.querySelector('.header-user-profile');
        if (userProfile) {
            headerList.insertBefore(themeToggleContainer, userProfile);
        } else {
            headerList.appendChild(themeToggleContainer);
        }

        console.log('✅ Botón toggle creado exitosamente');
    }

    // Crear el botón después de que el DOM esté listo
    createToggleButton();

    // Configurar event listeners para todos los botones toggle
    document.querySelectorAll('.theme-toggle-btn').forEach(btn => {
        btn.addEventListener('click', function (e) {
            e.preventDefault();
            e.stopPropagation();
            ThemeManager.toggleTheme();
        });
    });

    // Actualizar estado inicial del botón
    const currentTheme = document.body.classList.contains(DARK_MODE_CLASS) ? 'dark' : 'light';
    ThemeManager.updateToggleButton(currentTheme);

    // Agregar atajo de teclado (Ctrl/Cmd + Shift + D)
    document.addEventListener('keydown', function (e) {
        if ((e.ctrlKey || e.metaKey) && e.shiftKey && e.key === 'D') {
            e.preventDefault();
            ThemeManager.toggleTheme();
        }
    });

    window.ThemeManager = ThemeManager;

});