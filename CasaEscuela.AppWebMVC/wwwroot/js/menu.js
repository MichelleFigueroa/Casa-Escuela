// JavaScript para el archivo menu.js - Sobrescribir comportamiento del template
$(document).ready(function () {
        // Desactivar event listeners del template
        $('#sidebar-hide, #mobile-collapse').off('click');

        // Aplicar estado inicial
        initSidebar();

        // Agregar tus propios event listeners
        $('#sidebar-hide, #mobile-collapse').on('click', toggleSidebar);
        // ... resto del código ...
    // Función para detectar desktop
    function isDesktop() {
        return $(window).width() >= 1025;
    }

    // Aplicar estado inicial (colapsado en desktop)
    function initSidebar() {
        if (isDesktop()) {
            const savedState = localStorage.getItem('sidebar-state');
            if (savedState === 'expanded') {
                $('body').removeClass('pc-sidebar-collapse');
            } else {
                $('body').addClass('pc-sidebar-collapse');
                localStorage.setItem('sidebar-state', 'collapsed');
            }
        } else {
            // En móvil, siempre colapsado por defecto
            $('body').addClass('pc-sidebar-collapse');
            $('.pc-sidebar-overlay').remove();
        }
        // Aplicar el indicador visual después de establecer el estado
        updateToggleIndicator();
    }

    // Función toggle personalizada
    function toggleSidebar(e) {
        if (e) {
            e.preventDefault();
            e.stopPropagation();
        }

        const $body = $('body');
        const isCollapsed = $body.hasClass('pc-sidebar-collapse');

        if (isDesktop()) {
            // Comportamiento en desktop
            if (isCollapsed) {
                $body.removeClass('pc-sidebar-collapse');
                localStorage.setItem('sidebar-state', 'expanded');
            } else {
                $body.addClass('pc-sidebar-collapse');
                localStorage.setItem('sidebar-state', 'collapsed');
            }
        } else {
            // Comportamiento en móvil
            if (isCollapsed) {
                $body.removeClass('pc-sidebar-collapse');
                // Agregar overlay
                if (!$('.pc-sidebar-overlay').length) {
                    $('body').append('<div class="pc-sidebar-overlay"></div>');
                }
            } else {
                $body.addClass('pc-sidebar-collapse');
                $('.pc-sidebar-overlay').remove();
            }
        }
    }

    // Remover event listeners existentes del template y agregar los nuestros
    setTimeout(function () {
        $('#sidebar-hide, #mobile-collapse').off('click').on('click', toggleSidebar);
    }, 100);

    // Cerrar sidebar al hacer clic en overlay (móvil)
    $(document).on('click', '.pc-sidebar-overlay', function (e) {
        e.stopPropagation();
        $('body').addClass('pc-sidebar-collapse');
        $(this).remove();
    });

    // Manejar redimensionamiento de ventana
    let resizeTimeout;
    $(window).on('resize', function () {
        clearTimeout(resizeTimeout);
        resizeTimeout = setTimeout(function () {
            initSidebar();
        }, 150);
    });

    // Cerrar sidebar al hacer clic fuera (solo móvil)
    $(document).on('click', function (e) {
        if (isDesktop()) return;

        const $target = $(e.target);
        const $sidebar = $('.pc-sidebar');
        const $toggleBtns = $('#sidebar-hide, #mobile-collapse');

        // Verificar si el clic fue fuera del sidebar y botones
        if (!$sidebar.is($target) && !$sidebar.has($target).length &&
            !$toggleBtns.is($target) && !$toggleBtns.has($target).length &&
            !$target.hasClass('pc-sidebar-overlay') &&
            !$('body').hasClass('pc-sidebar-collapse')) {

            $('body').addClass('pc-sidebar-collapse');
            $('.pc-sidebar-overlay').remove();
        }
    });

    // Aplicar estado inicial
    initSidebar();

    // Funciones globales para usar desde otros scripts
    window.sidebarToggle = toggleSidebar;
    window.sidebarCollapse = function () {
        $('body').addClass('pc-sidebar-collapse');
        $('.pc-sidebar-overlay').remove();
        if (isDesktop()) {
            localStorage.setItem('sidebar-state', 'collapsed');
        }
    };
    window.sidebarExpand = function () {
        $('body').removeClass('pc-sidebar-collapse');
        if (!isDesktop() && !$('.pc-sidebar-overlay').length) {
            $('body').append('<div class="pc-sidebar-overlay"></div>');
        }
        if (isDesktop()) {
            localStorage.setItem('sidebar-state', 'expanded');
        }
    };

    // Indicador visual en el botón
    function updateToggleIndicator() {
        const $toggleBtns = $('#sidebar-hide, #mobile-collapse');
        const isCollapsed = $('body').hasClass('pc-sidebar-collapse');

        $toggleBtns.removeClass('sidebar-toggle-indicator');
        if (!isCollapsed) {
            $toggleBtns.addClass('sidebar-toggle-indicator');
        }
    }

    // Observer para cambios en la clase del body
    const observer = new MutationObserver(function (mutations) {
        mutations.forEach(function (mutation) {
            if (mutation.type === 'attributes' && mutation.attributeName === 'class') {
                updateToggleIndicator();
            }
        });
    });

    observer.observe(document.body, {
        attributes: true,
        attributeFilter: ['class']
    });

    // Aplicar indicador inicial
    updateToggleIndicator();
});