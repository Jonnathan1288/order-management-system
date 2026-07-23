export const environmentUrl = (dev: boolean) => `http://${dev ? 'localhost:5102' : 'localhost:5102'}/api/v1`;

export const environment = {
  production: false,
  API_URL: environmentUrl(false),
  DEFAULT_BUSINESS_ID: 1,
};
