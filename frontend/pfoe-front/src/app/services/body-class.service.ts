import {Injectable, Renderer2, RendererFactory2} from '@angular/core';
import {backgroundColor, black, fontSizeAttr, white} from "../constants/constants";
import {FontSize} from "../enums/font-size";
import {BgClass} from "../enums/bg-class";

@Injectable({
  providedIn: 'root'
})
export class BodyClassService {
  private renderer: Renderer2;
  private body: HTMLElement | null = null;
  private defaultFontSize = FontSize._16PX;
  private defaultBgClass = BgClass.LIGHT;

  constructor(rendererFactory: RendererFactory2) {
    this.renderer = rendererFactory.createRenderer(null, null);
    this.body = document.querySelector('body');
  }

  setStyles(classBg: string | undefined, fontSize: number | undefined) {
    if (classBg === BgClass.DARK) {
      this.renderer.setStyle(this.body, backgroundColor, black);
    } else {
      this.renderer.setStyle(this.body, backgroundColor, white);
    }
    if (fontSize && [FontSize._12PX, FontSize._16PX, FontSize._20PX].includes(fontSize)) {
      this.renderer.setStyle(this.body, fontSizeAttr, fontSize + 'px');
    }else {
      this.renderer.setStyle(this.body, fontSizeAttr, FontSize._16PX)
    }
  }

  removeClass(className: string) {
    this.renderer.removeClass(this.body, className);
  }
}
