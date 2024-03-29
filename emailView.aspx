﻿<!-- ***************************************************
********************************************************

HOW TO USE: Use these code examples as a guideline for formatting your HTML email. You may want to create your own template based on these snippets or just pick and choose the ones that fix your specific rendering issue(s). There are two main areas in the template: 1. The header (head) area of the document. You will find global styles, where indicated, to move inline. 2. The body section contains more specific fixes and guidance to use where needed in your design.

DO NOT COPY OVER COMMENTS AND INSTRUCTIONS WITH THE CODE to your message or risk spam box banishment :).

It is important to note that sometimes the styles in the header area should not be or don't need to be brought inline. Those instances will be marked accordingly in the comments.

********************************************************
**************************************************** -->

<!-- Using the xHTML doctype is a good practice when sending HTML email. While not the only doctype you can use, it seems to have the least inconsistencies. For more information on which one may work best for you, check out the resources below.

UPDATED: Now using xHTML strict based on the fact that gmail and hotmail uses it.  Find out more about that, and another great boilerplate, here: http://www.emailology.org/#1

More info/Reference on doctypes in email:
Campaign Monitor - http://www.campaignmonitor.com/blog/post/3317/correct-doctype-to-use-in-html-email/
Email on Acid - http://www.emailonacid.com/blog/details/C18/doctype_-_the_black_sheep_of_html_email_design
-->

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0"/>
	<title>Momjian, Garo - $500,000.00</title>
	<style type="text/css">

		/***********
		Originally based on The MailChimp Reset from Fabio Carneiro, MailChimp User Experience Design
		More info and templates on Github: https://github.com/mailchimp/Email-Blueprints
		http://www.mailchimp.com &amp; http://www.fabio-carneiro.com

		INLINE: Yes.
		***********/
		/* Client-specific Styles */
		#outlook a {padding:0;} /* Force Outlook to provide a "view in browser" menu link. */
		body{width:100% !important; -webkit-text-size-adjust:100%; -ms-text-size-adjust:100%; margin:0; padding:0; font-family:"Palatino Linotype", "Book Antiqua", Palatino, serif}
		/* Prevent Webkit and Windows Mobile platforms from changing default font sizes, while not breaking desktop design. */
		.ExternalClass {width:100%;} /* Force Hotmail to display emails at full width */
		.ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {line-height: 100%;} /* Force Hotmail to display normal line spacing.  More on that: http://www.emailonacid.com/forum/viewthread/43/ */
		#backgroundTable {margin:0; padding:0; width:100% !important; line-height: 100% !important;}
		/* End reset */

		/* Some sensible defaults for images
		1. "-ms-interpolation-mode: bicubic" works to help ie properly resize images in IE. (if you are resizing them using the width and height attributes)
		2. "border:none" removes border when linking images.
		3. Updated the common Gmail/Hotmail image display fix: Gmail and Hotmail unwantedly adds in an extra space below images when using non IE browsers. You may not always want all of your images to be block elements. Apply the "image_fix" class to any image you need to fix.

		Bring inline: Yes.
		*/
		img {outline:none; text-decoration:none; -ms-interpolation-mode: bicubic;}
		a img {border:none;}
		.image_fix {display:block;}

		/** Yahoo paragraph fix: removes the proper spacing or the paragraph (p) tag. To correct we set the top/bottom margin to 1em in the head of the document. Simple fix with little effect on other styling. NOTE: It is also common to use two breaks instead of the paragraph tag but I think this way is cleaner and more semantic. NOTE: This example recommends 1em. More info on setting web defaults: http://www.w3.org/TR/CSS21/sample.html or http://meiert.com/en/blog/20070922/user-agent-style-sheets/

		Bring inline: Yes.
		**/
		p {margin: 1em 0;}

		/** Hotmail header color reset: Hotmail replaces your header color styles with a green color on H2, H3, H4, H5, and H6 tags. In this example, the color is reset to black for a non-linked header, blue for a linked header, red for an active header (limited support), and purple for a visited header (limited support).  Replace with your choice of color. The !important is really what is overriding Hotmail's styling. Hotmail also sets the H1 and H2 tags to the same size.

		Bring inline: Yes.
		**/
		h1, h2, h3, h4, h5, h6 {color: black !important;}

		h1 a, h2 a, h3 a, h4 a, h5 a, h6 a {color: blue !important;}

		h1 a:active, h2 a:active,  h3 a:active, h4 a:active, h5 a:active, h6 a:active {
			color: red !important; /* Preferably not the same color as the normal header link color.  There is limited support for psuedo classes in email clients, this was added just for good measure. */
		 }

		h1 a:visited, h2 a:visited,  h3 a:visited, h4 a:visited, h5 a:visited, h6 a:visited {
			color: purple !important; /* Preferably not the same color as the normal header link color. There is limited support for psuedo classes in email clients, this was added just for good measure. */
		}

		/** Outlook 07, 10 Padding issue: These "newer" versions of Outlook add some padding around table cells potentially throwing off your perfectly pixeled table.  The issue can cause added space and also throw off borders completely.  Use this fix in your header or inline to safely fix your table woes.

		More info: http://www.ianhoar.com/2008/04/29/outlook-2007-borders-and-1px-padding-on-table-cells/
		http://www.campaignmonitor.com/blog/post/3392/1px-borders-padding-on-table-cells-in-outlook-07/

		H/T @edmelly

		Bring inline: No.
		**/
		table td {border-collapse: collapse;}

		/** Remove spacing around Outlook 07, 10 tables

		More info : http://www.campaignmonitor.com/blog/post/3694/removing-spacing-from-around-tables-in-outlook-2007-and-2010/

		Bring inline: Yes
		**/
		table { border-collapse:collapse; mso-table-lspace:0pt; mso-table-rspace:0pt; }

		/* Styling your links has become much simpler with the new Yahoo.  In fact, it falls in line with the main credo of styling in email, bring your styles inline.  Your link colors will be uniform across clients when brought inline.

		Bring inline: Yes. */
		a {color: orange;}

		/* Or to go the gold star route...
		a:link { color: orange; }
		a:visited { color: blue; }
		a:hover { color: green; }
		*/

		/***************************************************
		****************************************************
		MOBILE TARGETING

		Use @media queries with care.  You should not bring these styles inline -- so it's recommended to apply them AFTER you bring the other stlying inline.

		Note: test carefully with Yahoo.
		Note 2: Don't bring anything below this line inline.
		****************************************************
		***************************************************/

		/* NOTE: To properly use @media queries and play nice with yahoo mail, use attribute selectors in place of class, id declarations.
		table[class=classname]
		Read more: http://www.campaignmonitor.com/blog/post/3457/media-query-issues-in-yahoo-mail-mobile-email/
		*/
		@media only screen and (max-device-width: 480px) {

			/* A nice and clean way to target phone numbers you want clickable and avoid a mobile phone from linking other numbers that look like, but are not phone numbers.  Use these two blocks of code to "unstyle" any numbers that may be linked.  The second block gives you a class to apply with a span tag to the numbers you would like linked and styled.

			Inspired by Campaign Monitor's article on using phone numbers in email: http://www.campaignmonitor.com/blog/post/3571/using-phone-numbers-in-html-email/.

			Step 1 (Step 2: line 224)
			*/
			a[href^="tel"], a[href^="sms"] {
						text-decoration: none;
						color: black; /* or whatever your want */
						pointer-events: none;
						cursor: default;
					}

			.mobile_link a[href^="tel"], .mobile_link a[href^="sms"] {
						text-decoration: default;
						color: orange !important; /* or whatever your want */
						pointer-events: auto;
						cursor: default;
					}
		}

		/* More Specific Targeting */

		@media only screen and (min-device-width: 768px) and (max-device-width: 1024px) {
			/* You guessed it, ipad (tablets, smaller screens, etc) */

			/* Step 1a: Repeating for the iPad */
			a[href^="tel"], a[href^="sms"] {
						text-decoration: none;
						color: blue; /* or whatever your want */
						pointer-events: none;
						cursor: default;
					}

			.mobile_link a[href^="tel"], .mobile_link a[href^="sms"] {
						text-decoration: default;
						color: orange !important;
						pointer-events: auto;
						cursor: default;
					}
		}

		@media only screen and (-webkit-min-device-pixel-ratio: 2) {
			/* Put your iPhone 4g styles in here */
		}

		/* Following Android targeting from:
		http://developer.android.com/guide/webapps/targeting.html
		http://pugetworks.com/2011/04/css-media-queries-for-targeting-different-mobile-devices/  */
		@media only screen and (-webkit-device-pixel-ratio:.75){
			/* Put CSS for low density (ldpi) Android layouts in here */
		}
		@media only screen and (-webkit-device-pixel-ratio:1){
			/* Put CSS for medium density (mdpi) Android layouts in here */
		}
		@media only screen and (-webkit-device-pixel-ratio:1.5){
			/* Put CSS for high density (hdpi) Android layouts in here */
		}
		/* end Android targeting */
	</style>

	<!-- Targeting Windows Mobile -->
	<!--[if IEMobile 7]>
	<style type="text/css">

	</style>
	<![endif]-->

	<!-- ***********************************************
	****************************************************
	END MOBILE TARGETING
	****************************************************
	************************************************ -->

	<!--[if gte mso 9]>
	<style>
		/* Target Outlook 2007 and 2010 */
	</style>
	<![endif]-->
</head>
<body>
	<!-- Wrapper/Container Table: Use a wrapper table to control the width and the background color consistently of your email. Use this approach instead of setting attributes on the body tag. -->
	<table cellpadding="0" cellspacing="0" border="0" id="backgroundTable">
    <tr>
        <td>
            <table cellpadding="0" cellspacing="8" border="0" align="center">
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="8" border="0" align="center">
                            <tr>
                                <td>
                                    <br />
                                    <img width="254" height="44" title="" alt="" src="data:image/jpeg;base64,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" />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <hr />
            <table cellpadding="0" cellspacing="8" border="0" align="center" width="90%">
                <tr>
                    <td>
                        <p>Dear David R:</p>
                        <p>Attached please find the following case for your review:</p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="10" cellspacing="8" border="1" align="center" width="80%">
                            <tr>
                                <td><b>Insurer's Name:</b></td>
                                <td><b>Momjian, Garo</b></td>
			                </tr>
                            <tr>
                                <td><b>Insurer's Gender:</b></td>
                                <td><b>Male</b></td>
			                </tr>
                            <tr>
                                <td><b>Insurer's DOB:</b></td>
                                <td><b>9/29/1929</b></td>
			                </tr>
                            <tr>
                                <td><b>Insurer's Age:</b></td>
                                <td><b>84</b></td>
			                </tr>
                            <tr>
                                <td><b>Policy Number:</b></td>
                                <td><b>15515248</b></td>
			                </tr>
                            <tr>
                                <td><b>Death Benefit:</b></td>
                                <td><b>$500,000.00</b></td>
			                </tr>
                            <tr>
                                <td><b>Date of issue:</b></td>
                                <td><b>2/28/2001</b></td>
			                </tr>
                            <tr>
                                <td><b>Premium Financed:</b></td>
                                <td><b>NO</b></td>
			                </tr>
                            <tr>
                                <td><b>Insurance Company:</b></td>
                                <td><b>Mass Mutual</b></td>
			                </tr>
                            <tr>
                                <td><b>Premium:</b></td>
                                <td><b>$4,000.00</b></td>
			                </tr>
                            <tr>
                                <td><b>21st 50:</b></td>
                                <td><b>29</b></td>
			                </tr>
                            <tr>
                                <td><b>AVS:</b></td>
                                <td><b>55</b></td>
			                </tr>
                            <tr>
                                <td><b>Other LE:</b></td>
                                <td><b>50</b></td>
			                </tr>
                            <tr>
                                <td><b>Owner State:</b></td>
                                <td><b>California</b></td>
			                </tr>
		                </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="8" border="0" align="center" width="80%">
                            <tr>
                                <td>
                                    <p>The following files are available for download:</p>
                                    <ul><li>Momjian_BOR signed 20130923.pdf</li><li>Momjian_248 Ill 20130726.pdf</li><li>Momjian_248 VOC 20130726.pdf</li><li>Momjian_21st LE 20130820.pdf</li><li>Momjian_Meds Cancer Network.pdf</li></ul>
				                </td>
			                </tr>
		                </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br /><br />
                        <b>Please <a href="http://sharepoint.abacussettlements.com:6500/Review.aspx"> login here</a> to visit your account portal</b><br>
                    </td>
                </tr>
                <tr>
                    <td><p style="color:red;">DO NOT REPLY TO THIS E-MAIL!</p></td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
</body>
</html>
