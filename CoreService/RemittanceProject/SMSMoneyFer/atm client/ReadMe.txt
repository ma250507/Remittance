Software requirements
=====================
	DotNet Framework 2.0

Installing NCRDiitalSigner.dll assembly for VB6
================================================
	Just Run the Command RegAsm as follow:

	regasm  NCRDiitalSigner.dll  /tlb:NCRDiitalSigner.tlb  /codebase


Installing Certificate
=======================
+Start
	+Run >mmc
		+File > Add / Remove Snap - In ..
			+Add
				+Select Certificates from Snap-In list > Add
				+Select Computer Account > Next
				+Local Computer > Finish
				+Close
			+OK
		+Expand Certificates Node in the left tree
		+Expand Personal node
		+Right Click the details right related to Personal node
		+select Import from right click Menu
			+Next
			+Browse for the *.PFX file >next
			+Ignore Password request and Check Mark this key as exportable, >next
			+select the Personal store as the certificate store > next
			+Finish
			+Test using test Program ...
