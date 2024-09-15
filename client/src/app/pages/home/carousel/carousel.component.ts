import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

interface carouselImage{
  imageSrc:string;
  imageAlt:string;
}

@Component({
  selector: 'app-carousel',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.scss']
})
export class CarouselComponent implements OnInit {

  @Input() images: carouselImage[] = []
  @Input() indicators = true;
  @Input() controls = true;
  @Input() autoSlide = false;
  @Input() slideInterval = 1000;

  
  selectedIndex = 0;
  ngOnInit(): void {
    if(this.autoSlide){
      this.autoSlideImages();
    }
  }
  autoSlideImages():void{
    setInterval(()=>{
      this.onNextClick();
    },this.slideInterval);
  }


  selectImage(index: number):void{
    this.selectedIndex = index;
  }
  onPrevClick():void{
    if(this.selectedIndex===0){
      this.selectedIndex = this.images.length - 1;
    }else{
      this.selectedIndex--;
    }
  }
  onNextClick():void{
    if(this.selectedIndex === this.images.length - 1){
      this.selectedIndex = 0 ;
    }else{
      this.selectedIndex++;
    }
  }

}
