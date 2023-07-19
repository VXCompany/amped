export interface RequestConfigModel {
  url: string;
  method: string;
  options: {
    headers: {
      [index: string]: string;
    };
    body?: string;
  } 
}
