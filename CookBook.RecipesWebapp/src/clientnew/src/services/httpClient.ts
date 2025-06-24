import axios, {
  CustomParamsSerializer,
  GenericAbortSignal,
  Method,
  ParamsSerializerOptions,
  ResponseType,
} from 'axios';
import { appConfig } from '~/configuration/appConfig';
import { REQUEST_TIMEOUT } from '~/constants';

interface IAxiosConfiguration {
  baseUrl: string;
  withCredentialsParam: boolean;
}

interface IAxiosHeaders {
  [key: string]: string;
}

interface IConfiguration {
  url?: string;
  method?: Method | string;
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  params?: any;
  timeout?: number;
  additionalHeaders?: IAxiosHeaders;
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  data?: any;
  withCredentials?: boolean;
  signal?: GenericAbortSignal;
  responseType?: ResponseType;
  paramsSerializer?: ParamsSerializerOptions | CustomParamsSerializer;
}

const configureAxios = (axiosConfig: IAxiosConfiguration) => (clientConfig: IConfiguration) => {
  const headers: IAxiosHeaders = {
    'Content-Type': 'application/json',
    Pragma: 'no-cache',
    ...clientConfig.additionalHeaders,
  };

  return axios({
    baseURL: axiosConfig.baseUrl,
    url: clientConfig.url ?? '',
    method: clientConfig.method ?? 'get',
    params: clientConfig.params,
    timeout: clientConfig.timeout ?? REQUEST_TIMEOUT,
    headers: headers,
    data: clientConfig.data,
    withCredentials: clientConfig.withCredentials,
    signal: clientConfig.signal,
    responseType: clientConfig.responseType,
    paramsSerializer: clientConfig.paramsSerializer,
  });
};

export const httpClient = configureAxios({
  baseUrl: appConfig.API_URL,
  withCredentialsParam: true,
});
