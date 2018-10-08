const ROUTES = {
    DIAGRAMS: '#diagrams'
}

const DiagramUI = {
    Components: {
        Sidebar: {
            Classifiers: {
                addListeners: function() {
                    $('#classifiers li').on('click', function() {
                        const $el = $(this);
                        $el.addClass('active');

                        const name = DiagramUI.Components.PromptDialog.get('Name?');

                        if (!name) {
                            $el.removeClass('active');
                            return;
                        }

                        DiagramAPI.Resources.Classifiers
                            .create({ diagramId: DiagramUI.Diagrams.id }, {
                                name: name,
                                type: $el.data('type')
                            })
                            .then(classifier => {
                                DiagramUI.Classifiers.draw(classifier);
                                $el.removeClass('active');
                            });
                    });
                },

                hideControls: function() {
                    $('[data-target="#classifiers"]').hide();
                    $('[data-target="#classifiers"] > lu').hide();
                },

                showControls: function() {
                    $('[data-target="#classifiers"]').show();
                    $('[data-target="#classifiers"] > lu').show();
                }
            },
            Relationships: {
                hideControls: function() {
                    $('[data-target="#relationships"]').hide();
                    $('[data-target="#relationships"] > lu').hide();
                },

                showControls: function() {
                    $('[data-target="#relationships"]').show();
                    $('[data-target="#relationships"] > lu').show();
                }
            }
        },
        PromptDialog: {
            get: function(question) {
                return window.prompt(question);
            }
        }
        
    },

    Diagrams: {
        getIcon: function (diagram) {
            return $(`<div class="diagram-icon"><i class="fa fa-tags fa-lg"> ${diagram.name}</div>`)
                .on('click', function () {
                    App.renderDiagram(diagram.id);
                });
        },
        getNewIcon: function (diagram) {
            return $('<div class="diagram-icon create"><i class="fa fa-plus fa-3x"></div>')
                .on('click', function () {
                    const name = DiagramUI.Components.PromptDialog.get('Name?');

                    if (!name) return;

                    DiagramAPI.Resources.Diagrams
                        .create({}, { name: name })
                        .then(diagram => App.renderDiagram(diagram.id));
                });
        },
        getTitle: function (diagram) {
            return $(`<span>${diagram && diagram.name || 'All Diagrams'}</span>`)
                .on('click', function () {
                    App.renderDiagram(diagram.id);
                });
        },
        draw: function (diagram) {
            DiagramUI.Diagrams.id = diagram.id;
            DiagramUI.Diagrams.graph = new joint.dia.Graph;

            const paper = new joint.dia.Paper({
                el: document.getElementById('diagramHolder'),
                model: DiagramUI.Diagrams.graph,
                width: '100%',
                height: $(window).height() - 140,
                gridSize: 1
            });

            diagram.classifiers.forEach((classifier) => {
                DiagramUI.Classifiers.draw(classifier);
            });

            diagram.classifiers.forEach((classifier) => {
                classifier.relationships.forEach((relationship) => {
                    DiagramUI.Relationships.draw(relationship);
                });
            });
        }
    },

    Classifiers: {
        draw: function (classifier) {
            const options = {
                position: { x: 300, y: 300 },
                size: { width: 100, height: 100 },
                name: classifier.name,
            }

            let uiClassifier;
            
            switch (classifier.type) {
                case 1:
                    uiClassifier = new joint.shapes.uml.Class(options);
                    break;
                case 2:
                    uiClassifier = new joint.shapes.uml.Interface(options);
                    break;
                default:
                    uiClassifier = new joint.shapes.uml.Abstract(options);
                    break;
            };

            uiClassifier.addTo(DiagramUI.Diagrams.graph);
        }
    },

    Relationships: {
        draw: function (relationship) {
            const link = new joint.shapes.standard.Link();
            link.source(relationship.classifier);
            link.target(relationship.target);
            link.addTo(DiagramUI.Diagrams.Graph);
        }
    }
};

const App = {
    init: function () {
        const [route, diagramId] = window.location.hash.split('/');

        DiagramUI.Components.Sidebar.Classifiers.addListeners();

        if (route === ROUTES.DIAGRAMS && diagramId) {
            return App.renderDiagram(diagramId);
        }

        return App.renderDiagrams();
    },

    renderDiagram: function (diagramId) {
        window.location = `${ROUTES.DIAGRAMS}/${diagramId}`;

        DiagramUI.Components.Sidebar.Classifiers.showControls();
        DiagramUI.Components.Sidebar.Relationships.showControls();

        DiagramAPI.Resources.Diagrams
            .get({ diagramId: diagramId })
            .then(diagram => {
                $('header').html(DiagramUI.Diagrams.getTitle(diagram));
                DiagramUI.Diagrams.draw(diagram);
            });
    },

    renderDiagrams: function () {
        window.location = ROUTES.DIAGRAMS;

        DiagramUI.Components.Sidebar.Classifiers.hideControls();
        DiagramUI.Components.Sidebar.Relationships.hideControls();

        DiagramAPI.Resources.Diagrams
            .get()
            .then((diagrams) => {
                $('header').html(DiagramUI.Diagrams.getTitle());
                $('#diagramHolder').html(diagrams.map(diagram => DiagramUI.Diagrams.getIcon(diagram)));
                $('#diagramHolder').append(DiagramUI.Diagrams.getNewIcon());
            });
    },
};
