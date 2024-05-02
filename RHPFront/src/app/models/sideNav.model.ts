import { IconDefinition } from "@fortawesome/free-solid-svg-icons";

export interface INavToggle {
    screenWidth: number;
    collapsed: boolean;
  }
  
  export interface INavData {
    routeLink: string;
    icon: IconDefinition;
    label: string;
  
  }