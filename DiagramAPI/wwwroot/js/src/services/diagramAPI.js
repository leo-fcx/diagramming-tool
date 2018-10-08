const PATH_PARAMS = {
    DIAGRAM_ID: 'diagramId',
    CLASSIFIER_ID: 'classifierId',
    RELATIONSHIP_ID: 'relationshipId'
};

const BASE_URL_PATTERN = 'https://localhost:5001/api';
const DIAGRAMS_URL_PATTERN = `${BASE_URL_PATTERN}/diagrams/${PATH_PARAMS.DIAGRAM_ID}`;
const CLASSIFIERS_URL_PATTERN = `${DIAGRAMS_URL_PATTERN}/classifiers/${PATH_PARAMS.CLASSIFIER_ID}`;
const RELATIONSHIPS_URL_PATTERN = `${CLASSIFIERS_URL_PATTERN}/relationship/${PATH_PARAMS.RELATIONSHIP_ID}`;

const buildURL = function(urlPattern, params) {
    Object
        .values(PATH_PARAMS)
        .forEach(function(key){
            urlPattern = urlPattern.replace(key, (params && params[key]) || '');
        });
    return urlPattern;
};

const request = function(url, payload) {
    return fetch(url, payload).then(response => response.json());
};

const RestResource = function(urlPattern) {
    this.urlPattern = urlPattern;
    
    return {
        get: pathParams => request(buildURL(this.urlPattern, pathParams)),
    
        create: (pathParams, payload) => {
            return request(buildURL(this.urlPattern, pathParams), {
                method: 'POST',
                headers: { 'Content-Type': 'application/json; charset=utf-8' },
                body: JSON.stringify(payload),
            });
        },
    
        update: (pathParams, payload) => {
            return request(buildURL(this.urlPattern, pathParams), {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json; charset=utf-8' },
                body: JSON.stringify(payload),
            });
        },
    
        remove: pathParams =>
            request(buildURL(this.urlPattern, pathParams), { method: 'DELETE' }),
    };
};

const DiagramAPI = {
    Resources: {
        Diagrams: new RestResource(DIAGRAMS_URL_PATTERN),
        Classifiers: new RestResource(CLASSIFIERS_URL_PATTERN),
        Relationships: new RestResource(RELATIONSHIPS_URL_PATTERN),
    }
};
